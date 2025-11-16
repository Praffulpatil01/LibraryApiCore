
CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(300) NOT NULL,
    Author VARCHAR(200) NOT NULL,
    ISBN VARCHAR(20) NOT NULL,
    PublishedYear INT,
    AvailableCopies INT NOT NULL
);

CREATE TABLE Members (
    MemberId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Phone VARCHAR(30),
    JoinDate DATE NOT NULL
);


CREATE TABLE BorrowRecords (
    BorrowId INT IDENTITY(1,1) PRIMARY KEY,
    MemberId INT NOT NULL,
    BookId INT NOT NULL,
    BorrowDate DATE NOT NULL,
    ReturnDate DATE NULL,
    IsReturned BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    FOREIGN KEY (MemberId) REFERENCES Members(MemberId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);
