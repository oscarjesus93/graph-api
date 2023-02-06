CREATE PROCEDURE [dbo].[SP_NODO_CHILD_DELETE]
	@parent int,
	@id int
AS
BEGIN
	
	Declare @message As NVarchar(250) = '',
				    @severityError As Int = 12,
					@statusError As Int = 2			
				
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
			

			DELETE FROM nodo_children where id = @id and parent = @parent		
			
	
END
GO


