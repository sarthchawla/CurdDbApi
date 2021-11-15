Steps to publish a new version of the project:
--------------------------------------------
1. Navigate to the directory where your .sln file is. Run the following command.
	dotnet publish -c Release

2. Go to the Api folder and Build a Docker image
	docker build -t curd-db-api ./bin/release/netcoreapp3.1/publish

3. Tag and push the build Docker image to Heroku. Pushing Docker image might take some time.
	docker tag curd-db-api registry.heroku.com/curd-db-api/web
	docker push registry.heroku.com/curd-db-api/web

4. Release the Heroku container by executing the following command.
	heroku container:release web -a curd-db-api

5. Your API application is now online at the following link:
	https://curd-db-api.herokuapp.com/authors

Connection string:
------------------ 
```
Server=remotemysql.com;Port=3306;Database=2DpJw7Z4He;Uid=2DpJw7Z4He;Pwd=3pJF1QwfNU;
```



database first approach cmnd:
-----------------------------
```
dotnet ef dbcontext scaffold "Server=remotemysql.com;Port=3306;Database=2DpJw7Z4He;Uid=2DpJw7Z4He;Pwd=3pJF1QwfNU;" MySql.EntityFrameworkCore -o 2DpJw7Z4He -f
```

References:
---------------------------------
- https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html
- https://stackoverflow.com/questions/26676563/entity-framework-queryable-async
- https://stackoverflow.com/questions/52536588/your-startup-project-doesnt-reference-microsoft-entityframeworkcore-design
- https://www.connectionstrings.com/mysql-connector-net-mysqlconnection/
- https://faun.pub/deploy-net-core-api-to-heroku-for-free-2f7f651932c4