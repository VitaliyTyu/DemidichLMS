# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Копируем файлы проекта и восстанавливаем зависимости
COPY *.csproj .
RUN dotnet restore

# Копируем все файлы и собираем приложение
COPY . .
RUN dotnet publish -c Release -o /app

# Используем официальный образ .NET Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Копируем собранное приложение из образа build
COPY --from=build /app .

# Указываем порт, который будет использовать приложение
EXPOSE 80

# Команда для запуска приложения
ENTRYPOINT ["dotnet", "AutoDealershipLMS.dll"]