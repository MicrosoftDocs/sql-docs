
--<snippetEnableTrigger1>
USE AdventureWorks2012;
GO
DISABLE TRIGGER Person.uAddress ON Person.Address;
GO
ENABLE Trigger Person.uAddress ON Person.Address;
GO
--</snippetEnableTrigger1>

--<snippetEnableTrigger2>
IF EXISTS (SELECT * FROM sys.triggers
    WHERE parent_class = 0 AND name = 'safety')
DROP TRIGGER safety ON DATABASE;
GO
CREATE TRIGGER safety 
ON DATABASE 
FOR DROP_TABLE, ALTER_TABLE 
AS 
   PRINT 'You must disable Trigger "safety" to drop or alter tables!' 
   ROLLBACK;
GO
DISABLE TRIGGER safety ON DATABASE;
GO
ENABLE TRIGGER safety ON DATABASE;
GO
--</snippetEnableTrigger2>

--<snippetEnableTrigger3>
USE AdventureWorks2012;
GO
ENABLE Trigger ALL ON ALL SERVER;
GO
--</snippetEnableTrigger3>


--<snippetDisableTrigger1>
USE AdventureWorks2012;
GO
DISABLE TRIGGER Person.uAddress ON Person.Address;
GO
--</snippetDisableTrigger1>

--<snippetDisableTrigger2>
IF EXISTS (SELECT * FROM sys.triggers
    WHERE parent_class = 0 AND name = 'safety')
DROP TRIGGER safety ON DATABASE;
GO
CREATE TRIGGER safety 
ON DATABASE 
FOR DROP_TABLE, ALTER_TABLE 
AS 
   PRINT 'You must disable Trigger "safety" to drop or alter tables!' 
   ROLLBACK;
GO
DISABLE TRIGGER safety ON DATABASE;
GO
--</snippetDisableTrigger2>

--<snippetDisableTrigger3>
USE AdventureWorks2012;
GO
DISABLE Trigger ALL ON ALL SERVER;
GO
--</snippetDisableTrigger3>
