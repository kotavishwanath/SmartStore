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
	}
}