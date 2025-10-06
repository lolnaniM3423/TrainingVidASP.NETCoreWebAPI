DELIMITER $$
CREATE PROCEDURE `sp_GetAllComments`(
    IN p_Symbol VARCHAR(10),
    IN p_IsDescending BOOLEAN
)
BEGIN
    SELECT
        c.*,
        u.*
    FROM Comments c
    LEFT JOIN AspNetUsers u ON c.AppUserId = u.Id
    LEFT JOIN Stocks s ON c.StockId = s.Id
    WHERE
        (p_Symbol IS NULL OR s.Symbol = p_Symbol)
    ORDER BY
        CASE WHEN p_IsDescending = 1 THEN c.CreatedOn END DESC,
        CASE WHEN p_IsDescending = 0 THEN c.CreatedOn END ASC;
END$$
DELIMITER ;