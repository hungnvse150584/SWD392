using Dapper;
using System;
using System.Linq;
using BPKS.Models;

public class UserRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public UserRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public Users RegisterUser(string userName, string password, string name, string email, string phoneNumber, string avatarUrl)
    {
        using (var connection = _dbConnectionFactory.CreateConnection())
        {
            // Tạo đối tượng Users
            var user = new Users
            {
                UserName = userName,
                Password = password,
            };

            // Mở kết nối đến cơ sở dữ liệu
            connection.Open();

            // Sử dụng Dapper để thêm người dùng mới vào bảng Users
            user.UserId = connection.Query<int>(
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

            connection.Execute(
                "INSERT INTO UsersDetail (UserId, Name, Email, PhoneNumber, AvatarUrl, CreatedDate) VALUES (@UserId, @Name, @Email, @PhoneNumber, @AvatarUrl, @CreatedDate);",
                userDetails
            );

            // Đóng kết nối
            connection.Close();

            return user;
        }
    }
}
