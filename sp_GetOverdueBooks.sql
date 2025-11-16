Create procedure sp_GetOverdueBooks
    @AsOfDate DATE = NULL,
    @GraceDays INT = 14 
AS
BEGIN
    DECLARE @date DATE = COALESCE(@AsOfDate, CAST(GETDATE() AS DATE));

    SELECT
        br.BorrowId,
        br.MemberId,
        m.Name        AS MemberName,
        m.Email       AS MemberEmail,
        br.BookId,
        b.Title       AS BookTitle,
        b.Author      AS BookAuthor,
        br.BorrowDate,
        DATEADD(day, @GraceDays, br.BorrowDate) AS DueDate,
        DATEDIFF(day, DATEADD(day, @GraceDays, br.BorrowDate), @date) AS DaysOverdue
    FROM dbo.BorrowRecords br
    JOIN dbo.Members m ON br.MemberId = m.MemberId
    JOIN dbo.Books b   ON br.BookId = b.BookId
    WHERE br.IsReturned = 0
      AND DATEADD(day, @GraceDays, br.BorrowDate) < @date
    ORDER BY DaysOverdue DESC, br.BorrowDate;
END;
