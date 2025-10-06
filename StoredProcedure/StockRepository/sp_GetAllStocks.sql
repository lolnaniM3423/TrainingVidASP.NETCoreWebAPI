DELIMITER $$
CREATE PROCEDURE `sp_GetAllStocks`(
    IN p_CompanyName VARCHAR(255),
    IN p_Symbol VARCHAR(10),
    IN p_SortBy VARCHAR(10),
    IN p_IsDescending BOOLEAN,
    IN p_Skip INT,
    IN p_PageSize INT
)
BEGIN
    SELECT * FROM Stocks
    WHERE
        (p_CompanyName IS NULL OR CompanyName LIKE CONCAT('%', p_CompanyName, '%'))
    AND
        (p_Symbol IS NULL OR Symbol LIKE CONCAT('%', p_Symbol, '%'))
    ORDER BY
        CASE WHEN p_SortBy = 'Symbol' AND p_IsDescending = 0 THEN Symbol END ASC,
        CASE WHEN p_SortBy = 'Symbol' AND p_IsDescending = 1 THEN Symbol END DESC
    LIMIT p_Skip, p_PageSize;
END$$
DELIMITER ;