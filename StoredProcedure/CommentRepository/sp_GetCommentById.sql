DELIMITER $$
CREATE PROCEDURE `sp_GetCommentById`(
    IN p_Id INT
)
BEGIN
    SELECT
        c.*,
        u.* FROM Comments c
    LEFT JOIN AspNetUsers u ON c.AppUserId = u.Id
    WHERE c.Id = p_Id;
END$$
DELIMITER ;