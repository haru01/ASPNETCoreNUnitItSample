
### 必要なもの
* Sqlite4
* dotnet

### WebAPIを CURLから確認

```

cd WebApi
dotnet restore
dotnet ef database update
dotnet run
curl -XGET http://localhost:5175/blogs | jq .
curl -XGET http://localhost:5175/blogs/1 | jq .

```

### テストから確認

```
cd WebApiInMemoryDBTests
dotnet restore
dotnet test
```
