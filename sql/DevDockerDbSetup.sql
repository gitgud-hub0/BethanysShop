USE master
GO
DROP DATABASE IF EXISTS PieShop
GO

CREATE DATABASE PieShop;

USE master
GO
CREATE LOGIN [docker_dbuser] WITH PASSWORD='dockerdev', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO
USE PieShop;
GO
CREATE USER [docker_dbuser] FOR LOGIN [docker_dbuser];
GO
EXEC sp_addrolemember N'db_owner',  [docker_dbuser];