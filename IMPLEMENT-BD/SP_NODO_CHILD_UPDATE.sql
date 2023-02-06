
CREATE PROCEDURE [dbo].[SP_NODO_CHILD_UPDATE]
	@id int,
	@parent int,
	@title nvarchar(150) = ''
AS
BEGIN
	
	Declare @message As NVarchar(250) = '',
				    @severityError As Int = 12,
					@statusError As Int = 2

			IF(@title = '')

				BEGIN
                
					Set @message = 'el titulo no debe estar vacio'
					Raiserror(@message, @severityError, @statusError)
					Return

				END	
				
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
			

			UPDATE nodo_children SET title = @title WHERE id = @id And parent = @parent	

			SELECT TOP 1
				[id], [parent], [title], [created_at] 
			FROM 
				[nodo_children]
			WHERE id = @id And parent = @parent
	
END
GO


