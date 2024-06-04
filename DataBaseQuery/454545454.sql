INSERT INTO Users (Username, [Password], Email, FirstName, LastName, BirthDate, PhoneNumber, [Address], RegistrationDate, UserStatus)
VALUES ('john_doe', 'securepassword123', 'john.doe@example.com', 'John', 'Doe', '1990-01-15', '123-456-7890', '123 Main St, Springfield, IL', GETDATE(), 'active');


select * from Users