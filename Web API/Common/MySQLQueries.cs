namespace smartStoreApi.Common
{
    public static class MySQLQueries
    {

		public const string InsertUserQuery = @"INSERT INTO users(CreatedDate
																	,CreatedBy
																	,FirstName
																	,LastName
																	,Gender
																	,Email
																	,Password
																	,Phone
																	,Address1
																	,Address2
																	,PostCode
																	,County
																	,Country)
																	Values(Now()
																		,'admin'
																		,@FirstName
																		,@LastName
																		,@Gender
																		,@Email
																		,@Password
																		,@Phone
																		,@Address1
																		,@Address2
																		,@PostCode
																		,@County
																		,@Country)";

        public const string GetUserByEmailQuery = @"SELECT Users.id
															,Users.firstName
															,Users.lastName
															,Users.email
															,Users.password
														FROM Users
														WHERE Users.email = @email;";

        public const string GetUserByIdQuery = @"SELECT Users.id
														,Users.name
														,Users.email
														,Users.password
														,Users.user_cat usercategoryid
														,userCategory.RU_CATEGORY_DESC usercategoryname
														,Users.mc_id mcId
													FROM Users
													INNER JOIN ref_usercategory userCategory ON Users.user_cat = userCategory.RU_CATEGORY_CODE
													WHERE Users.Id = @id
														AND Users.user_cat IN (
															51
															,52
															);";

        public const string ChangeUserPasswordQuery = @"UPDATE Users
														SET password = @Password,
															updated_at=NOW(),
															password_changed_at=NOW()
														WHERE Id = @id;

														INSERT INTO password_histories (
														`user_id`
														,`password`
														,`created_at`
														)
														VALUES (
														 @id
														,@Password
														,NOW()
														);";

		public const string GetProductsQuery = @"SELECT pc.Id CategoryId,
															pc.Name CategoryName,
															p.Id ProductId,
															p.Name ProductName,
															p.Price ProductPrice,
															p.ImagePath ProductImagePath,
															p.Description ProductDescription	
													FROM productcategory pc
													INNER JOIN product p ON pc.Id = p.CategoryId
													WHERE pc.IsActive = true";

		public const string GetUserViewedProductsQuery = @"SELECT pc.Id CategoryId,
																	pc.Name CategoryName,
																	p.Id ProductId,
																	p.Name ProductName,
																	p.Price ProductPrice,
																	p.ImagePath ProductImagePath,
																	p.Description ProductDescription
													FROM smartstore.userviewedproducts vp
													INNER JOIN product p on vp.ProductId = p.Id
													INNER JOIN productcategory pc ON pc.Id = p.CategoryId
													where vp.UserId = @userId
													order by vp.Count desc
													limit 10;";

		public const string GetProductDetailsQuery = @"SELECT p.Id ProductId,
															p.Name ProductName,
															p.Price ProductPrice,
															p.ImagePath ProductImagePath,
															p.Description ProductDescription
															FROM product p
															WHERE Id = @productId";

		public const string GetSuggestedProductsQuery = @"SELECT pc.Id CategoryId, pc.Name CategoryName,p.Id ProductId,
																p.Name ProductName,
																p.Price ProductPrice,
																p.ImagePath ProductImagePath,
																p.Description ProductDescription
																FROM product p
																inner JOIN productcategory pc ON pc.Id = p.CategoryId
																where Pc.Id= @categoryId;";

		public const string InsertUserViewedProductQuery = @"INSERT INTO userviewedproducts(UserId, ProductId, Count)
													VALUES(@userId, @productId, IFNULL(Count, 0) + 1)";

		public const string UpdateUserViewedProductQuery = @"UPDATE userviewedproducts SET Count = IFNULL(Count, 0) + 1
															WHERE UserId = @userId AND ProductId = @productId";

		public const string GetUserViewedProductByProductId = "SELECT * from userviewedproducts WHERE UserId = @userId AND ProductId = @productId";
	}
}