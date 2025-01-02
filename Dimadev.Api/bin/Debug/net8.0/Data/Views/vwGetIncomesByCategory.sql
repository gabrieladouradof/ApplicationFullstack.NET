CREATE OR REPLACE VIEW vwGetIncomesByCategory AS
    SELECT
        Transactions.UserId,
        Categories.Title AS Categories,
        YEAR (Transactions.PaidOrReceivedAt) AS `Year`,
         SUM(Transactions.Amount) AS Expenses
    FROM
        Transactions
            INNER JOIN Categories
                       ON Transactions.CategoryId = Categories.Id
    WHERE
        Transactions.PaidOrReceivedAt
            >= DATE_ADD(CURDATE(), INTERVAL -11 MONTH)
      AND Transactions.PaidOrReceivedAt
        < DATE_ADD(CURDATE(), INTERVAL 1 MONTH)
      AND Transactions.Type = 1
    GROUP BY
        Transactions.UserId,
        Categories.Title,
        YEAR(Transactions.PaidOrReceivedAt);