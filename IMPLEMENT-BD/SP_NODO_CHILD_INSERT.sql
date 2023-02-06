CREATE PROCEDURE [dbo].[SP_NODO_CHILD_INSERT]
	@parent int
AS
BEGIN
	
	Declare @message As NVarchar(250) = '',
				    @severityError As Int = 12,
					@statusError As Int = 2,
					@valorid as int = 0
				
			IF(@parent = 0)

				BEGIN
					Set @message = 'Debe indicar el id del nodo padre'
					Raiserror(@message, @severityError, @statusError)
					Return
				END

			IF NOT Exists(SELECT ID FROM nodo_father WHERE id = @parent)
				BEGIN
					Set @message = 'El id del nodo padre no existe'
					Raiserror(@message, @severityError, @statusError)
					Return
				END
			

			INSERT INTO nodo_children ( parent, created_at)
			VALUES
			   (@parent, getdate())

			SELECT TOP 1 @valorid = [ID] FROM [nodo_children] ORDER BY [id] DESC

			UPDATE nodo_children
				SET title = CONVERT(varchar(10), @valorid)
			WHERE [ID] = @valorid

			SELECT
				[id], [parent], [title], [created_at] 
			FROM 
				[nodo_children]
			WHERE [ID] = @valorid
	
END
GO


