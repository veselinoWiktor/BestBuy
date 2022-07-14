using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Constants
{
    public class SqlCommands
    {
        public const string Insert_Procedure = "create procedure InsertData @brandName nvarchar(50), @url nvarchar(200), @rate int as declare @id int IF EXISTS(SELECT 1 FROM Brands WHERE BrandName LIKE '%'+SUBSTRING(@brandName,1,3)+'%') Begin set @id=(SELECT Id FROM Brands WHERE BrandName LIKE '%'+SUBSTRING(@brandName,1,3)+'%') IF NOT EXISTS(select 1 from Pictures where Url = @url) insert into Pictures(Url, Rate, BrandId) values(@url, @rate, @id) end else begin insert into Brands(BrandName) values(@brandName) insert into Pictures(Url, Rate, BrandId) values(@url, @rate, IDENT_CURRENT('Brands')) end";
        public const string Create_Tables_Brands_Pictures = "CREATE TABLE Brands (Id int IDENTITY(1,1) PRIMARY KEY CLUSTERED, BrandName varchar(50) null) CREATE TABLE Pictures(Id int IDENTITY(1, 1) PRIMARY KEY CLUSTERED, Url nvarchar(500) null, Rate int null, BrandId int FOREIGN KEY REFERENCES Brands(Id))";
        public const string Select_Top_10 = "select top 10 b.BrandName, SUM(p.Rate) as Rate from Brands as b join Pictures as p on p.BrandId = b.Id group by b.BrandName order by Rate desc--";
        public const string Select_WebPictures = " select BrandName, Url, Rate from Brands as b join Pictures as p on p.BrandId = b.Id";
       // public const string Select_WebPictures = "SELECT b.BrandName, pr.Rate, pr.Url FROM Brands b join(SELECT p.Url, p.BrandId, p.Rate FROM Pictures as p) as pr on b.Id =pr.BrandId order by b.BrandName";
        public const string Select_ById = "select * from Brands as b join Pictures as p on p.BrandId = b.Id where b.Id=@id";
        public const string Select_ByName = "select * from Brands as b join Pictures as p on p.BrandId = b.Id where b.BrandName LIKE @name";
    }
}
