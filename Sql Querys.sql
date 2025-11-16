SELECT TOP (5)
    b.BookId,
    b.Title,
    b.Author,
    COUNT(br.BorrowId) AS BorrowCount
FROM dbo.BorrowRecords br
JOIN dbo.Books b ON br.BookId = b.BookId
GROUP BY b.BookId, b.Title, b.Author
ORDER BY COUNT(br.BorrowId) DESC;


SELECT
    m.MemberId,
    m.Name,
    m.Email,
    COUNT(br.BorrowId) AS BorrowCount
FROM dbo.BorrowRecords br
JOIN dbo.Members m ON br.MemberId = m.MemberId
WHERE br.BorrowDate >= DATEADD(day, -30, CAST(GETDATE() AS date))
GROUP BY m.MemberId, m.Name, m.Email
HAVING COUNT(br.BorrowId) > 3
ORDER BY BorrowCount DESC;

DECLARE @MemberId INT = 3;

SELECT
    br.BorrowId,
    br.BookId,
    b.Title,
    b.Author,
    br.BorrowDate,
    br.ReturnDate,
    br.IsReturned
FROM dbo.BorrowRecords br
JOIN dbo.Books b ON br.BookId = b.BookId
WHERE br.MemberId = @MemberId
  AND br.IsReturned = 0
ORDER BY br.BorrowDate DESC;
