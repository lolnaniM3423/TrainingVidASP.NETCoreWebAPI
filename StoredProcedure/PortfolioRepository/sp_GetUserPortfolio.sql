DELIMITER $$
CREATE PROCEDURE `sp_GetUserPortfolio`(
    IN p_AppUserId VARCHAR(255)
)
BEGIN
    SELECT s.* FROM Stocks s
    INNER JOIN Portfolios p ON s.Id = p.StockId
    WHERE p.AppUserId = p_AppUserId;
END$$
DELIMITER ;
