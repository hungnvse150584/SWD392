using Booking.Data.EF;
using Booking.Data.Entities;
using BookingSolution.Utilities.Exceptions;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.Common;
using BookingSolution.ViewModels.System.Services;
using BookingSolution.ViewModels.System.Users;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Firebase.Storage;
using Google.Apis.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Net.Sockets;

namespace Booking.Application.Catalog.Parties
{
    public class ManagePartyService : IManagePartyService
    {
        private readonly BookingDbContext _context;
        private static string Bucket = "bpks-ee4a1.appspot.com";
        public ManagePartyService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(PartyCreateRequest request)
        {
            var listroom = request.RoomId;
            var listproduct = request.ProductId;
            var party = new Party()
            {
                PartyName = request.PartyName,
                Description = request.Description,
                PhoneContact = request.PhoneContact,
                Place = request.Place,
                Rate = 0,
                CreatedDate = DateTime.Now,
                DayStart = request.DayStart,
                DayEnd = request.DayEnd,
                PartyHostId = request.PartyHostId,
                PartyStatus = "Pending",
                ThumbnailUrl = await this.SaveFile(request.ThumbnailUrl),

            };
            _context.Parties.Add(party);
            _context.SaveChanges();

            var partyrequest = _context.Parties.FirstOrDefault(p => p.ThumbnailUrl == party.ThumbnailUrl);
           
            foreach (var listRoom in listroom)
            {
                var add = new ListRoom
                {
                    PartyId = partyrequest?.PartyId,
                    RoomId = listRoom,
                    ListRoomStatus = "Pending",
                    ParentId = null,

                };
                _context.ListRooms.Add(add);
                foreach (var listProduct in listproduct)
                {
                    var addproduct = new ListProduct
                    {
                        PartyId = partyrequest?.PartyId,
                        RoomId = listRoom,
                        ProductId = listProduct,
                        ListProductStatus = "Pending",
                        Quantity = 0,

                    };
                    _context.ListProducts.Add(addproduct);
                }
            }
            
            if (partyrequest != null)
            {
                var listparty = new ListParty
                {
                    PartyHostId = request.PartyHostId,
                    PartyId = partyrequest?.PartyId,
                    ListPartyStatus= "Pending",
                };

                _context.ListParties.Add(listparty);
            }
            await _context.SaveChangesAsync();

            return partyrequest.PartyId;
        }

        public async Task<int> Delete(int partyId)
        {
            var party = await _context.Parties.FindAsync(partyId);
            if (party == null)
            {
                throw new BookingException($"Cannot find a party: {partyId}");
            }

            if (party.PartyStatus == "Pending")
            {
                var listParties = await _context.ListParties.Where(lp => lp.PartyId == partyId).ToListAsync();
                if (listParties != null && listParties.Any())
                {
                    _context.ListParties.RemoveRange(listParties);
                }

                var listRoom = await _context.ListRooms.Where(lr => lr.PartyId == partyId).ToListAsync();
                if (listRoom != null && listRoom.Any())
                {
                    _context.ListRooms.RemoveRange(listRoom);
                }

                var listProduct = await _context.ListProducts.Where(lp => lp.PartyId == partyId).ToListAsync();
                if (listProduct != null && listProduct.Any())
                {
                    _context.ListProducts.RemoveRange(listProduct);
                }

                _context.Parties.Remove(party);
            }
            else
            {
                throw new BookingException($"Party is not pending: {partyId}");
            }

            return await _context.SaveChangesAsync();
        }


        public async Task<List<PartyVm>> GetAll()
        {
            var party = await _context.Parties
                .Select(p => new PartyVm
                {
                    PartyId = p.PartyId,
                    PartyHostId = p.PartyHostId,
                    PartyName = p.PartyName,
                    Description = p.Description,
                    PhoneContact = p.PhoneContact,
                    Place = p.Place,
                    Rate = p.Rate,
                    ThumbnailUrl = p.ThumbnailUrl,
                    PartyStatus = p.PartyStatus,
                    DayStart = p.DayStart,
                    DayEnd = p.DayEnd,
                    CreatedDate = p.CreatedDate
                })
                .ToListAsync();
            party = party.Where(x => x.PartyStatus == "Active").ToList();
            return party;
        }

        public async Task<List<PartyHistory>> PartyHostHistory(PartyHistoryRequest request)
        {
            var query =
                from u in _context.AspNetUsers
                    //join lparent in _context.ListParties on u.Id equals lparent.ParentId
                join lpartyhost in _context.ListParties on u.Id equals lpartyhost.PartyHostId
                join p in _context.Parties on lpartyhost.PartyId equals p.PartyId
                
                where u.Id == request.user
                select new { u, p };

            if(!request.status.IsNullOrEmpty())
                query =query.Where(x => x.p.PartyStatus == request.status);

            var data = query.Select(t => new PartyHistory
            {
                PartyId = t.p.PartyId,
                CreatedDate = t.p.CreatedDate,
                DayEnd = t.p.DayEnd,
                DayStart = t.p.DayStart,
                Description = t.p.Description,
                PartyName = t.p.PartyName,
                PhoneContact = t.p.PhoneContact,
                Place = t.p.Place,
                ThumbnailUrl = t.p.ThumbnailUrl,
                Rate = t.p.Rate,
                PartyStatus = t.p.PartyStatus
            }).ToList();

            return data;
        }

        public async Task<List<PartyHistory>> ParentHistory(PartyHistoryRequest request)
        {
            var query =
                from u in _context.AspNetUsers
                //join lparent in _context.ListParties on u.Id equals lparent.ParentId
                //join lpartyhost in _context.ListParties on u.Id equals lpartyhost.PartyHostId
                join lr in _context.ListRooms on u.Id equals lr.ParentId
                join p in _context.Parties on lr.PartyId equals p.PartyId
                
                where u.Id == request.user 
                select new { u, p,lr };

            if (!request.status.IsNullOrEmpty())
                query = query.Where(x => x.p.PartyStatus == request.status);
            
            var data = query.Select(t => new PartyHistory
            {
                PartyId = t.p.PartyId,
                CreatedDate = t.p.CreatedDate,
                DayEnd = t.p.DayEnd,
                DayStart = t.p.DayStart,
                Description = t.p.Description,
                PartyName = t.p.PartyName,
                PhoneContact = t.p.PhoneContact,
                Place = t.p.Place,
                ThumbnailUrl = t.p.ThumbnailUrl,
                Rate = t.p.Rate,
                PartyStatus = t.lr.ListRoomStatus,
                RoomId = t.lr.RoomId,
                Total = t.lr.Total,
            }).ToList();

            return data;
        }

        public async Task<PagedResult<PartyVm>> GetAllPaging(GetPublicPartyPagingRequest request)
        {
            var query =
                from p in _context.Parties
                select new { p };

            //2. filter
            if (!string.IsNullOrEmpty(request.PartyName))
            {
                query = query.Where(x => x.p.PartyName.Contains(request.PartyName));
            }

            if (!string.IsNullOrEmpty(request.Place))
            {
                query = query.Where(x => x.p.Place.Contains(request.Place));
            }
            if(!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(x => x.p.PartyStatus.Contains(request.Status));
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PartyVm()
                {
                    PartyId = x.p.PartyId,
                    PartyHostId = x.p.PartyHostId,
                    PartyName = x.p.PartyName,
                    Description = x.p.Description,
                    PhoneContact = x.p.PhoneContact,
                    Place = x.p.Place,
                    Rate = x.p.Rate,
                    ThumbnailUrl = x.p.ThumbnailUrl,
                    PartyStatus = x.p.PartyStatus,
                    DayStart = x.p.DayStart,
                    DayEnd = x.p.DayEnd,
                    CreatedDate = x.p.CreatedDate

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<PartyVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<int> Update(PartyUpdateRequest request)
        {
            var party = await _context.Parties.FindAsync(request.PartyId);
            if (party == null)
            {
                throw new Exception($"Cannot find a party with id:{request.PartyId}.");
            }

            party.PartyName = request.PartyName != null ? request.PartyName : party.PartyName;
            party.PhoneContact = request.PhoneContact != null ? request.PhoneContact : party.PhoneContact;
            party.Place = request.Place != null ? request.Place : party.Place;
            party.ThumbnailUrl = request.ThumbnailImage != null ? await this.SaveFile(request.ThumbnailImage) : party.ThumbnailUrl;
            party.DayEnd = request.DayEnd != null ? request.DayEnd : party.DayEnd;
            //party.PartyStatus = request.PartyStatus != null ? request.PartyStatus : party.PartyStatus;
            party.PartyStatus = "Pending";
            party.Description = request.Description != null ? request.Description : party.Description;
            if (request.RoomId != null && request.RoomId.Any())
            {
                // Clear existing list of rooms
                party.ListRooms.Clear();

                // Add rooms from request
                foreach (var roomId in request.RoomId)
                {
                    var room = await _context.Rooms.FindAsync(roomId);
                    if (room != null)
                    {
                        party.ListRooms.Add(new ListRoom { RoomId = roomId, ListRoomStatus = "Pending"});
                    }
                    
                }
            }

            // Update ListProducts
            if (request.ProductId != null && request.ProductId.Any())
            {
                // Clear existing list of products
                party.ListProducts.Clear();

                // Add products from request
                foreach (var productId in request.ProductId)
                {
                    var product = await _context.Products.FindAsync(productId);
                    if (product != null)
                    {
                        party.ListProducts.Add(new ListProduct { ProductId = productId,ListProductStatus="Pending" });
                    }
                }
            }

            return await _context.SaveChangesAsync();
        }

      

        public async Task<int> UpdatePartyDetails (PartyDetailsUpdateRequest request)
        {
            
            var data = await GetPartyDetail(request.PartyId);
    
            var listroomdif = new List<int>();
         
            if (data == null)
            {
                return 0;
            }
            else
            {
                var party = await _context.Parties.FindAsync(request.PartyId);
                party.PartyName = request.PartyName != null ? request.PartyName : party.PartyName;
                party.PhoneContact = request.PhoneContact != null ? request.PhoneContact : party.PhoneContact;
                party.Place = request.Place != null ? request.Place : party.Place;
                //party.ThumbnailUrl = request.ThumbnailUrl != null ? await this.SaveFile(request.ThumbnailUrl) : party.ThumbnailUrl;
                party.DayEnd = request.DayEnd != null ? request.DayEnd : party.DayEnd;
                party.Description = request.Description != null ? request.Description : party.Description;
                party.PartyStatus = "Pending";
                _context.SaveChanges();
                

                foreach(var item in data.roomUserViews)
                {
                    if (request.roomUserViews.FirstOrDefault(x => x.RoomId == item.RoomId) == null)
                    {
                        listroomdif.Add(item.RoomId);
                    }
                }

                if(listroomdif.Count > 0)
                {
                    var listroom = _context.ListRooms.ToList();
                    var listproduct = _context.ListProducts.ToList();
                    
                    foreach(var item in listroomdif)
                    {
                        var room = await _context.ListRooms.FirstOrDefaultAsync(x => x.PartyId == request.PartyId&&x.RoomId == item);
                        var product = await _context.ListProducts.FirstOrDefaultAsync(x => x.PartyId == request.PartyId && x.RoomId == item);
                        if (room != null)
                        {
                            _context.ListRooms.Remove(room);
                        }
                        if(product != null)
                        {
                            _context.ListProducts.Remove(product);
                        }
                    }
                       
                    
                }
                if (request.roomUserViews.Count > 0)
                {
                    var output = request.roomUserViews.GroupBy(x=> x.RoomId);
                    foreach (var item in output)
                    {
                        var listroom = new ListRoom
                        {
                            PartyId = request.PartyId,
                            RoomId = item.Key,
                            ListRoomStatus = "Pending"
                        };
                        _context.ListRooms.Add(listroom);
                        foreach (var p in item)
                        {
                            var product = new ListProduct
                            {
                                ListProductStatus = "Pending",
                                ProductId = p.ProductId,
                                PartyId = request.PartyId,
                                RoomId = item.Key,
                            };
                            _context.ListProducts.Add(product);
                        }
                    }

                }

               

            }


            return await _context.SaveChangesAsync();
        }


        private async Task<string> SaveFile(IFormFile file)
        {

            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var task = new FirebaseStorage(Bucket,
               new FirebaseStorageOptions
               {
                   ThrowOnCancel = true
               })
               .Child("images")
               .Child("PartyImage")
               .Child(fileName)
               .PutAsync(file.OpenReadStream());
            return await task;
        }

        public async Task<PartyVm> GetById(int partyId)
        {
            var p = await _context.Parties.FindAsync(partyId);
            if (p == null)
            {
                return null;
            }
            

            var roomVm = new PartyVm
            {
                PartyId = p.PartyId,
                PartyHostId = p.PartyHostId,
                PartyName = p.PartyName,
                Description = p.Description,
                PhoneContact = p.PhoneContact,
                Place = p.Place,
                Rate = p.Rate,
                ThumbnailUrl = p.ThumbnailUrl,
                PartyStatus = p.PartyStatus,
                DayStart = p.DayStart,
                DayEnd = p.DayEnd,
                CreatedDate = p.CreatedDate
            };

            return roomVm;
        }

        public async Task<PartyUserView> GetPartyDetail(int partyid)
        {
            PartyUserView partydetail = new PartyUserView();
            var query =
                from party in _context.Parties
                where party.PartyId == partyid
                from listroom in _context.ListRooms.Where(lr => lr.PartyId == party.PartyId).DefaultIfEmpty()
                from room in _context.Rooms.Where(rt => listroom.RoomId == rt.RoomId).DefaultIfEmpty()
                from listproduct in _context.ListProducts.Where(lp => lp.RoomId == room.RoomId).DefaultIfEmpty()
                from product in _context.Products.Where(p1 => listproduct.ProductId == p1.ProductId).DefaultIfEmpty()
                select new { party, listroom, room, listproduct, product };

            foreach (var item in query)
            {
                if (item.party.PartyId != partydetail.PartyId)
                {
                    partydetail.PartyId = item.party.PartyId;
                    partydetail.PartyName = item.party.PartyName;
                    partydetail.DayStart = item.party.DayStart;
                    partydetail.DayEnd = item.party.DayEnd;
                    partydetail.PhoneContact = item.party.PhoneContact;
                    partydetail.Description = item.party.Description;
                    partydetail.Place = item.party.Place;
                    partydetail.Rate = item.party.Rate;
                    partydetail.ThumbnailUrl = item.party.ThumbnailUrl;
                    partydetail.roomUserViews = new List<RoomUserView>();
                    if (item.room != null)
                    {
                        partydetail.roomUserViews.Add(new RoomUserView
                        {
                            RoomId = item.room.RoomId,
                            Price = item.room.Price,
                            RoomName = item.room.RoomName,
                            RoomUrl = item.room.RoomUrl,
                            RoomType = item.room.RoomType,
                            productUserViews = new List<ProductUserView>()
                        }) ;
                    }

                }
                if (partydetail.roomUserViews.Count() > 0)
                {
                    if (item.room.RoomId != partydetail.roomUserViews.Last().RoomId)
                    {
                        var room = new RoomUserView
                        {
                            RoomId = item.room.RoomId,
                            Price = item.room.Price,
                            RoomName = item.room.RoomName,
                            RoomUrl = item.room.RoomUrl,
                            RoomType = item.room.RoomType,
                            productUserViews = new List<ProductUserView>(),
                        };
                        partydetail.roomUserViews.Add(room);
                    }
                    if (item.room.RoomId == partydetail.roomUserViews.Last().RoomId)
                    {
                        //partydetail.roomUserViews.Last().productUserViews = new List<ProductUserView>();
                        var product = new ProductUserView
                        {
                            ProductId = item.product.ProductId,
                            ProductName = item.product.ProductName,
                            ProductUrl = item.product.ProductUrl,
                            ProductType = item.product.ProductType,
                            Description = item.product.Description,
                            Price = item.product.Price,
                            ProductStyle = item.product.ProductStyle
                        };
                        partydetail.roomUserViews.Last().productUserViews.Add(product);
                    }

                    if (partydetail.roomUserViews.Last().productUserViews.Count() > 0)
                    {
                        if (item.product.ProductId != partydetail.roomUserViews.Last().productUserViews.Last().ProductId)
                        {
                            var product = new ProductUserView
                            {
                                ProductId = item.product.ProductId,
                                Description = item.product.Description,
                                Price = item.product.Price,
                                ProductName = item.product.ProductName,
                                ProductUrl = item.product.ProductUrl,
                                ProductType = item.product.ProductType,
                                ProductStyle = item.product.ProductStyle,

                            };
                            partydetail.roomUserViews.Last().productUserViews.Add(product);
                        }

                    }

                }

            }

            return partydetail;

        }

        public Task<List<PartyVm>> GetPartyWithStatus(GetPartyWithStatus request)
        {
            var query =
                from p in _context.Parties
                join lp in _context.ListParties on p.PartyHostId equals lp.PartyHostId
                where p.PartyStatus == request.Status
                select new { p, lp };

            if (request.Id != Guid.Empty)
                query = query.Where(x => x.lp.PartyHostId == request.Id);
            var data = query.Select(x => new PartyVm()
            {
                PartyId = x.p.PartyId,
                PartyHostId = x.p.PartyHostId,
                PartyName = x.p.PartyName,
                Description = x.p.Description,
                PhoneContact = x.p.PhoneContact,
                Place = x.p.Place,
                Rate = x.p.Rate,
                ThumbnailUrl = x.p.ThumbnailUrl,
                PartyStatus = x.p.PartyStatus,
                DayStart = x.p.DayStart,
                DayEnd = x.p.DayEnd,
                CreatedDate = x.p.CreatedDate

            }).ToListAsync();

            return data;

        }

        public async Task<int> UpdatePartyStatus(UpdatePartyStatusRequest request)
        {
            var party = await _context.Parties.FindAsync(request.PartyId);
            if (party == null)
            {
                throw new Exception($"Cannot find a party with id:{request.PartyId}.");
            }
            party.PartyStatus = request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Approve(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                throw new Exception($"Cannot find a party with id:{id}.");
            }
            party.PartyStatus = "Approve";

            return await _context.SaveChangesAsync();
        }
        public async Task<int> Rejected(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                throw new Exception($"Cannot find a party with id:{id}.");
            }
            party.PartyStatus = "Rejected";

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<PartyVm>> GetPartyApprove(GetPublicPartyPagingRequest request)
        {
            var query =
                from p in _context.Parties
                where p.PartyStatus == "Approve" // Filter by PartyStatus
                select new { p };

            // Filter by PartyName if provided
            if (!string.IsNullOrEmpty(request.PartyName))
            {
                query = query.Where(x => x.p.PartyName.Contains(request.PartyName));
            }

            // Filter by Place if provided
            if (!string.IsNullOrEmpty(request.Place))
            {
                query = query.Where(x => x.p.Place.Contains(request.Place));
            }

            // Count total records
            int totalRow = await query.CountAsync();

            // Apply paging and projection
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PartyVm()
                {
                    PartyId = x.p.PartyId,
                    PartyHostId = x.p.PartyHostId,
                    PartyName = x.p.PartyName,
                    Description = x.p.Description,
                    PhoneContact = x.p.PhoneContact,
                    Place = x.p.Place,
                    Rate = x.p.Rate,
                    ThumbnailUrl = x.p.ThumbnailUrl,
                    PartyStatus = x.p.PartyStatus,
                    DayStart = x.p.DayStart,
                    DayEnd = x.p.DayEnd,
                    CreatedDate = x.p.CreatedDate
                })
                .ToListAsync();

            // Create the paged result
            var pagedResult = new PagedResult<PartyVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<PartyVm>> GetPartyPartyHostView(GetPublicPartyPagingRequest request)
        {
            var query =
         from p in _context.Parties
         where p.PartyHostId == request.PartyHostId // Filter by PartyStatus and PartyHostId
         select new { p };

            // Filter by PartyName if provided
            if (!string.IsNullOrEmpty(request.PartyName))
            {
                query = query.Where(x => x.p.PartyName.Contains(request.PartyName));
            }

            // Filter by Place if provided
            if (!string.IsNullOrEmpty(request.Place))
            {
                query = query.Where(x => x.p.Place.Contains(request.Place));
            }

            // Count total records
            int totalRow = await query.CountAsync();

            // Apply paging and projection
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PartyVm()
                {
                    PartyId = x.p.PartyId,
                    PartyHostId = x.p.PartyHostId,
                    PartyName = x.p.PartyName,
                    Description = x.p.Description,
                    PhoneContact = x.p.PhoneContact,
                    Place = x.p.Place,
                    Rate = x.p.Rate,
                    ThumbnailUrl = x.p.ThumbnailUrl,
                    PartyStatus = x.p.PartyStatus,
                    DayStart = x.p.DayStart,
                    DayEnd = x.p.DayEnd,
                    CreatedDate = x.p.CreatedDate
                })
                .ToListAsync();

            // Create the paged result
            var pagedResult = new PagedResult<PartyVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<int> FeedBack(FeedbackRequest request)
        {
            var query =
                from lp in _context.ListParties
                join p in _context.Parties on lp.PartyId equals p.PartyId
                where lp.ParentId == request.ParentId 
                && p.PartyId == request.PartyId
                select lp;
           var fb = _context.Feedbacks.FirstOrDefault(x=> x.ParentId == request.ParentId && x.PartyId == request.PartyId);
            if (fb == null) return 0;
            if (query.Count() > 0 && fb != null)
            {
                var feedback = new Feedback
                {
                    PartyId = request.PartyId,
                    Feedback1 = request.Feedback,
                    ParentId = request.ParentId,
                    Score = request.Score,
                };
                _context.Feedbacks.Add(feedback);
                _context.SaveChanges();

                var fquery =
                from f in _context.Feedbacks
                join p in _context.Parties on f.PartyId equals p.PartyId
                where f.PartyId == request.PartyId
                select new { f, p };

                if(fquery.Count() > 0)
                {
                    int row = fquery.Count();
                    var total = fquery.Sum(s => s.f.Score);

                    var party = await _context.Parties.FindAsync(request.PartyId);
                    if (party != null)
                    {
                        party.Rate = Math.Round((double)total / row, 1);
                    }
                }

               
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> ComfirmParty(int partyId)
        {
            var party = _context.Parties.Find(partyId);
            if(party!= null)
            {
                party.PartyStatus = "Active";

                var lp = _context.ListParties.Where(p => p.PartyId == partyId).ToList();
                lp.ForEach(p => p.ListPartyStatus = "Active");
                var lr = _context.ListRooms.Where(p => p.PartyId == partyId).ToList();
                lr.ForEach(p => p.ListRoomStatus = "Active");
                var lpr = _context.ListProducts.Where(p => p.PartyId == partyId).ToList();
                lpr.ForEach(p => p.ListProductStatus = "Active");
                //party.ListParties.ToList().ForEach(p =>  p.ListPartyStatus = "Active");
                //party.ListRooms.ToList().ForEach(p => p.ListRoomStatus = "Active");
                //party.ListProducts.ToList().ForEach(p => p.ListProductStatus = "Active");
            }


            return await _context.SaveChangesAsync();
        }

        public async Task<int> CheckOut(int partyId)
        {
            var party = _context.Parties.Find(partyId);
            if (party != null)
            {
                party.PartyStatus = "Overdue";
                var lp = _context.ListParties.Where(p => p.PartyId == partyId).ToList();
                lp.ForEach(p => p.ListPartyStatus = "Overdue");
                var lr = _context.ListRooms.Where(p => p.PartyId == partyId).ToList();
                lr.ForEach(p => p.ListRoomStatus = "Overdue");
                var lpr = _context.ListProducts.Where(p => p.PartyId == partyId).ToList();
                lpr.ForEach(p => p.ListProductStatus = "Overdue");
            }


            return await _context.SaveChangesAsync();
        }


        public async Task<PartyUserView> DetailsRoomBooked(DetailsRoomBookedRequest request)
        {
            PartyUserView partydetail = new PartyUserView();
            var query =
                from party in _context.Parties
                where party.PartyId == request.partyId
                from listroom in _context.ListRooms.Where(lr => lr.PartyId == party.PartyId).DefaultIfEmpty()
                from room in _context.Rooms.Where(rt => listroom.RoomId == rt.RoomId).DefaultIfEmpty()
                from listproduct in _context.ListProducts.Where(lp => lp.RoomId == room.RoomId).DefaultIfEmpty()
                from product in _context.Products.Where(p1 => listproduct.ProductId == p1.ProductId).DefaultIfEmpty()
                where room.RoomId == request.roomId
                select new { party, listroom, room, listproduct, product };

            foreach (var item in query)
            {
                if (item.party.PartyId != partydetail.PartyId)
                {
                    partydetail.PartyId = item.party.PartyId;
                    partydetail.PartyName = item.party.PartyName;
                    partydetail.DayStart = item.party.DayStart;
                    partydetail.DayEnd = item.party.DayEnd;
                    partydetail.PhoneContact = item.party.PhoneContact;
                    partydetail.Description = item.party.Description;
                    partydetail.Place = item.party.Place;
                    partydetail.Rate = item.party.Rate;
                    partydetail.ThumbnailUrl = item.party.ThumbnailUrl;
                    partydetail.roomUserViews = new List<RoomUserView>();
                    if (item.room != null)
                    {
                        partydetail.roomUserViews.Add(new RoomUserView
                        {
                            RoomId = item.room.RoomId,
                            Price = item.room.Price,
                            RoomName = item.room.RoomName,
                            RoomUrl = item.room.RoomUrl,
                            RoomType = item.room.RoomType,
                            productUserViews = new List<ProductUserView>()
                        });
                    }

                }
                if (partydetail.roomUserViews.Count() > 0)
                {
                    if (item.room.RoomId != partydetail.roomUserViews.Last().RoomId)
                    {
                        var room = new RoomUserView
                        {
                            RoomId = item.room.RoomId,
                            Price = item.room.RoomId,
                            RoomName = item.room.RoomName,
                            RoomUrl = item.room.RoomUrl,
                            RoomType = item.room.RoomType,
                            productUserViews = new List<ProductUserView>(),
                        };
                        partydetail.roomUserViews.Add(room);
                    }
                    if (item.room.RoomId == partydetail.roomUserViews.Last().RoomId)
                    {
                        //partydetail.roomUserViews.Last().productUserViews = new List<ProductUserView>();
                        var product = new ProductUserView
                        {
                            ProductId = item.product.ProductId,
                            ProductName = item.product.ProductName,
                            ProductUrl = item.product.ProductUrl,
                            ProductType = item.product.ProductType,
                            Description = item.product.Description,
                            Price = item.product.Price,
                            ProductStyle = item.product.ProductStyle,
                            Quantity = item.listproduct.Quantity,
                        };
                        partydetail.roomUserViews.Last().productUserViews.Add(product);
                    }

                    if (partydetail.roomUserViews.Last().productUserViews.Count() > 0)
                    {
                        if (item.product.ProductId != partydetail.roomUserViews.Last().productUserViews.Last().ProductId)
                        {
                            var product = new ProductUserView
                            {
                                ProductId = item.product.ProductId,
                                Description = item.product.Description,
                                Price = item.product.Price,
                                ProductName = item.product.ProductName,
                                ProductUrl = item.product.ProductUrl,
                                ProductType = item.product.ProductType,
                                ProductStyle = item.product.ProductStyle,
                                Quantity = item.listproduct.Quantity,
                            };
                            partydetail.roomUserViews.Last().productUserViews.Add(product);
                        }

                    }

                }

            }

            return partydetail;
        }

        //public async Task<List<RoomVm>> GetRoomsByPartyId(int partyId)
        //{
        //    var rooms = await _context.ListRooms
        //        .Where(lr => lr.PartyId == partyId)
        //        .Select(lr => new RoomVm
        //        {
        //            RoomId = lr.RoomId,
        //            // Include other properties you need from the Room entity
        //        })
        //        .ToListAsync();

        //    return rooms;
        //}
    }
}