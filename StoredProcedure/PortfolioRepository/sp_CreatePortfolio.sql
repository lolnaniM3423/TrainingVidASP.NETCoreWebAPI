DELIMITER $$
CREATE PROCEDURE `sp_CreatePortfolio`(
    IN p_AppUserId VARCHAR(255),
    IN p_StockId INT
)
BEGIN
    INSERT INTO Portfolios (AppUserId, StockId)
    VALUES (p_AppUserId, p_StockId);
END$$
DELIMITER ;
