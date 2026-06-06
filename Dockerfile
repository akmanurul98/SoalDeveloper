# 1. Gunakan SDK .NET 9 resmi sebagai environment untuk BUILD
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# 2. Salin file .csproj dan lakukan restore dependencies (nuget packages)
# Jika file .csproj berada di dalam folder yang sama, perintah ini akan mendeteksinya secara otomatis
COPY *.csproj ./
RUN dotnet restore

# 3. Salin seluruh source code proyek ke dalam container
COPY . ./

# 4. Lakukan publish proyek ke dalam folder bernama 'out' dengan konfigurasi Release
RUN dotnet publish -c Release -o out

# 5. Gunakan Runtime ASP.NET 9 resmi (lebih ringan) untuk menjalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# 6. Salin hasil build (dari folder 'out' di stage build-env) ke stage runtime ini
COPY --from=build-env /app/out .

# 7. Konfigurasi agar aplikasi berjalan di port yang disediakan oleh Render
# Render secara default menggunakan port 8080 untuk container non-root .NET 9
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# 8. Tentukan perintah utama untuk menjalankan aplikasi kamu
ENTRYPOINT ["dotnet", "SoalDeveloper.dll"]