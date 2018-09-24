use COMPRE
GO

create table Customer
(
	CustomerID varchar(5) not null primary key,
	CustomerName nvarchar(40) not null,
	Country varchar(50)
)
GO

alter procedure SP_CUstomer_CRUD
@Action varchar(10), @CustomerID varchar(5), @CustomerName nvarchar(40), @Country varchar(50)
AS
BEGIN
	if(@Action = 'CREATE')
		INSERT INTO Customer Values(@CustomerID, @CustomerName, @Country)
	else if(@Action = 'UPDATE')
		UPDATE Customer SET CustomerName = @CustomerName, Country = @Country WHERE CustomerID = @CustomerID
	else
		DELETE FROM Customer WHERE CustomerID = @CustomerID
END
GO