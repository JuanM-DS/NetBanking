Create database NetBankingDb
Use NetBankingDb

CREATE TABLE Users (
    UserId int PRIMARY KEY identity not null,
    Username VARCHAR(50) NOT NULL UNIQUE,
    [Password] VARCHAR(255) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    BirthDate DATE,
    PhoneNumber VARCHAR(20),
    [Address] VARCHAR(100),
    RegistrationDate DATE not null, 
    UserStatus VARCHAR(20),
);


CREATE TABLE SavingsAccounts (
    AccountId INT IDENTITY(1,1) PRIMARY KEY not null,
    AccountNumber VARCHAR(20) NOT NULL UNIQUE,
    UserId INT NOT NULL,
    Balance DECIMAL(15, 2) NOT NULL,
    OpeningDate DATE NOT NULL,
    AccountStatus VARCHAR(20),
    InterestRate DECIMAL(5, 2),
    LastTransactionDate DATETIME,
    DailyWithdrawalLimit DECIMAL(15, 2) not null,
	ProductType varchar(50),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

CREATE TABLE CurrentAccounts (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    AccountNumber VARCHAR(20) NOT NULL UNIQUE,
    UserId INT NOT NULL,
    Balance DECIMAL(15, 2) NOT NULL,
    OpeningDate DATE NOT NULL,
    AccountStatus VARCHAR(20),
    DailyWithdrawalLimit DECIMAL(15, 2) not null,
    LastTransactionDate DATETIME,
	ProductType varchar(50),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

CREATE TABLE CreditCards (
    CardId INT IDENTITY(1,1) PRIMARY KEY,
    CardNumber VARCHAR(20) NOT NULL UNIQUE,
    UserId INT NOT NULL,
    ExpiryDate DATE NOT NULL,
    CreditLimit DECIMAL(15, 2) NOT NULL,
    AvailableBalance DECIMAL(15, 2) NOT NULL,
    CreditCardStatus VARCHAR(20),
	ProductType varchar(50),
	LastTransactionDate DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

drop table CreditCards

CREATE TABLE Loans (
    LoanId INT IDENTITY(1,1) PRIMARY KEY not null,
    UserId INT NOT NULL,
    LoanAmount DECIMAL(15, 2) NOT NULL,
    InterestRate DECIMAL(5, 2) NOT NULL,
    LoanTermMonths INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    MonthlyPayment DECIMAL(15, 2),
    LoanStatus VARCHAR(20),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
);

CREATE TABLE Checks (
    ChequeId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    ChequeNumber VARCHAR(20) NOT NULL UNIQUE,
    Amount DECIMAL(15, 2) NOT NULL,
    IssuedDate DATE NOT NULL,
	IssuerName VARCHAR(100) NOT NULL, 
    ReceiverName VARCHAR(100) NOT NULL, 
    CheckStatus VARCHAR(20),
    FOREIGN KEY (AccountId) REFERENCES CurrentAccounts(AccountId),
);

CREATE TABLE Vouchers (
    VoucherId INT IDENTITY(1,1) PRIMARY KEY,
    TransactionDate DATE NOT NULL,
    Amount DECIMAL(15, 2) NOT NULL,
    [Description] VARCHAR(255),
	IssuerUserId INT NOT NULL,
    ReceiverUserId INT NOT NULL,
	DestinationIdentifier VARCHAR(20),
    FOREIGN KEY (IssuerUserId) REFERENCES Users(UserId),
    FOREIGN KEY (ReceiverUserId) REFERENCES Users(UserId),
);

CREATE TABLE BankTransactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    TransactionDate DATETIME NOT NULL,
    TransactionType VARCHAR(50) NOT NULL,
    Amount DECIMAL(15, 2) NOT NULL,
    [Description] VARCHAR(255),
    IssuerUserId INT NOT NULL,
	OriginIdentifier VARCHAR(20),
    ReceiverUserId INT NOT NULL,
	DestinationIdentifier VARCHAR(20),
    FOREIGN KEY (IssuerUserId) REFERENCES Users(UserId),
    FOREIGN KEY (ReceiverUserId) REFERENCES Users(UserId),
);


Create table UsersLogin(
UserLoginId INT IDENTITY(1,1) PRIMARY KEY not null,
[UserName] varchar(50) not null,
[Password] varchar(250) not null,
FirstName varchar(50) not null,
[Role] varchar(50) not null
)

INSERT INTO Users (Username, [Password], Email, FirstName, LastName, BirthDate, PhoneNumber, [Address], RegistrationDate, UserStatus)
VALUES ('john_doe', 'securepassword123', 'john.doe@example.com', 'John', 'Doe', '1990-01-15', '123-456-7890', '123 Main St, Springfield, IL', GETDATE(), 'active');

select * from Users