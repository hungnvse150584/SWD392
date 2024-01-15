using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;

namespace BPKS.Models
{
    public class UserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(string connectionString)
        {
            _dbConnection = new MySqlConnection(connectionString);
        }

        public Users RegisterUser(string userName, string password, string name, string email, string phoneNumber, string avatarUrl)
        {
            // Tạo đối tượng Users
            var user = new Users
            {
                UserName = userName,
                Password = password,
            };

            // Mở kết nối đến cơ sở dữ liệu
            _dbConnection.Open();

            // Sử dụng Dapper để thêm người dùng mới vào bảng Users
            user.UserId = _dbConnection.Query<int>(
                "INSERT INTO Users (UserName, Password) VALUES (@UserName, @Password); SELECT LAST_INSERT_ID();",
                user
            ).FirstOrDefault();

            // Thêm thông tin chi tiết người dùng vào bảng UsersDetail
            var userDetails = new UsersDetail
            {
                UserId = user.UserId,
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                AvatarUrl = avatarUrl,
                CreatedDate = DateTime.Now
            };

            _dbConnection.Execute(
                "INSERT INTO UsersDetail (UserId, Name, Email, PhoneNumber, AvatarUrl, CreatedDate) VALUES (@UserId, @Name, @Email, @PhoneNumber, @AvatarUrl, @CreatedDate);",
                userDetails
            );

            // Đóng kết nối
            _dbConnection.Close();

            return user;
        }
    }
}
