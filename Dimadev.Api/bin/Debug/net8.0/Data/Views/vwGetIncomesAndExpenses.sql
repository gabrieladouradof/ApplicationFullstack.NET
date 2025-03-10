CREATE OR REPLACE VIEW vwGetIncomesAndExpenses AS
    SELECT
        Transactions.UserId,        
        Month(Transactions.PaidOrReceivedAt) as `Month`,
        Year(Transactions.PaidOrReceivedAt) as `Year`,
        SUM(CASE WHEN Transactions.Type = 1 THEN Transactions.Amount ELSE 0 END) AS Incomes,
        SUM(CASE WHEN Transactions.Type = 2 THEN Transactions.Amount ELSE 0 END) AS Expenses
        
    FROM Transactions
    WHERE
        Transactions.PaidOrReceivedAt
            >= DATE_ADD(CURDATE(), INTERVAL -11 MONTH)
      AND Transactions.PaidOrReceivedAt
        < DATE_ADD(CURDATE(), INTERVAL -11 MONTH)
    GROUP BY
        Transactions.UserId,
        MONTH(Transactions.PaidOrReceivedAt),
        YEAR(Transactions.PaidOrReceivedAt);