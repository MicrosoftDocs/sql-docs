---
title: "Move a TDE-protected database to another SQL Server"
description: Describes how to protect a database using transparent data encryption (TDE) and then move the database to another instance of SQL Server using SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL).
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 01/19/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Transparent Data Encryption, moving"
  - "TDE, moving a database"
---
# Move a TDE protected database to another SQL Server

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to protect a database by using transparent data encryption (TDE), and then move the database to another instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../../includes/tsql-md.md)]. TDE performs real-time I/O encryption and decryption of the data and log files. The encryption uses a database encryption key (DEK), which is stored in the database boot record for availability during recovery. The DEK is a symmetric key secured by using a certificate stored in the `master` database of the server or an asymmetric key protected by an EKM module.

## <a id="Restrictions"></a> Limitations

- When moving a TDE protected database, you must also move the certificate or asymmetric key that is used to open the DEK. The certificate or asymmetric key must be installed in the `master` database of the destination server, so that [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] can access the database files. For more information, see [Transparent data encryption (TDE)](transparent-data-encryption.md).

- You must retain copies of both the certificate file and the private key file in order to recover the certificate. The password for the private key doesn't have to be the same as the database master key password.

- [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] stores the files created here in `C:\Program Files\Microsoft SQL Server\MSSQL<xx>.MSSQLSERVER\MSSQL\DATA` by default, where `<xx>` is the version number.

## Permissions

- Requires `CONTROL DATABASE` permission on the `master` database to create the database master key.

- Requires `CREATE CERTIFICATE` permission on the `master` database to create the certificate that protects the DEK.

- Requires `CONTROL DATABASE` permission on the encrypted database and `VIEW DEFINITION` permission on the certificate or asymmetric key that is used to encrypt the database encryption key.

## <a id="SSMSProcedure"></a> Create a database protected by Transparent Data Encryption

The following procedures show you how to create a database protected by TDE using SQL Server Management Studio and by using Transact-SQL.

### <a id="SSMSCreate"></a> Use SQL Server Management Studio

1. Create a database master key and certificate in the `master` database. For more information, see **Using Transact-SQL** later in this article.

1. Create a backup of the server certificate in the `master` database. For more information, see **Using Transact-SQL** later in this article.

1. In Object Explorer, right-click the **Databases** folder and select **New Database**.

1. In the **New Database** dialog box, in the **Database name** box, enter the name of the new database.

1. In the **Owner** box, enter the name of the new database's owner. Alternately, select the ellipsis **(...)** to open the **Select Database Owner** dialog box. For more information on creating a new database, see [Create a database](../../databases/create-a-database.md).

1. In Object Explorer, select the plus sign to expand the **Databases** folder.

1. Right-click the database you created, point to **Tasks**, and select **Manage Database Encryption**.

     The following options are available on the **Manage Database Encryption** dialog box.

     **Encryption Algorithm**  
     Displays or sets the algorithm to use for database encryption. **AES128** is the default algorithm. This field can't be blank. For more information on encryption algorithms, see [Choose an encryption algorithm](choose-an-encryption-algorithm.md).

     **Use server certificate**  
     Sets the encryption to be secured by a certificate. Select one from the list. If you don't have the `VIEW DEFINITION` permission on server certificates, this list is empty. If a certificate method of encryption is selected, this value can't be empty. For more information about certificates, see [SQL Server Certificates and Asymmetric Keys](../sql-server-certificates-and-asymmetric-keys.md).

     **Use server asymmetric key**  
     Sets the encryption to be secured by an asymmetric key. Only available asymmetric keys are displayed. Only an asymmetric key protected by an EKM module can encrypt a database using TDE.

     **Set Database Encryption On**  
     Alters the database to turn on (checked) or turn off (unchecked) TDE.

1. When finished, select **OK**.

### <a id="TsqlCreate"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   -- Create a database master key and a certificate in the master database.
   USE master;
   GO

   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '*rt@40(FL&dasl1';
   GO

   CREATE CERTIFICATE TestSQLServerCert
       WITH SUBJECT = 'Certificate to protect TDE key'
   GO

   -- Create a backup of the server certificate in the master database.
   -- The following code stores the backup of the certificate and the private key file in the default data location for this instance of SQL Server
   -- (C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA).
   BACKUP CERTIFICATE TestSQLServerCert TO FILE = 'TestSQLServerCert'
   WITH PRIVATE KEY (
       FILE = 'SQLPrivateKeyFile',
       ENCRYPTION BY PASSWORD = '*rt@40(FL&dasl1'
   );
   GO

   -- Create a database to be protected by TDE.
   CREATE DATABASE CustRecords;
   GO

   -- Switch to the new database.
   -- Create a database encryption key, that is protected by the server certificate in the master database.
   -- Alter the new database to encrypt the database using TDE.
   USE CustRecords;
   GO

   CREATE DATABASE ENCRYPTION KEY
       WITH ALGORITHM = AES_128 ENCRYPTION BY SERVER CERTIFICATE TestSQLServerCert;
   GO

   ALTER DATABASE CustRecords
   SET ENCRYPTION ON;
   GO
   ```

For more information, see:

- [CREATE MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-master-key-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../../t-sql/statements/create-certificate-transact-sql.md)
- [BACKUP CERTIFICATE (Transact-SQL)](../../../t-sql/statements/backup-certificate-transact-sql.md)
- [CREATE DATABASE](../../../t-sql/statements/create-database-transact-sql.md)
- [CREATE DATABASE ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-database-encryption-key-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../../t-sql/statements/alter-database-transact-sql.md)

## <a id="TsqlProcedure"></a> Move a database protected by Transparent Data Encryption

The following procedures show you how to move a database protected by TDE using SQL Server Management Studio and by using Transact-SQL.

### <a id="SSMSMove"></a> Use SQL Server Management Studio

1. In Object Explorer, right-click the database you encrypted previously, point to **Tasks** and select **Detach...**.

   The following options are available in the **Detach Database** dialog box.

   **Databases to detach**  
   Lists the databases to detach.

   **Database Name**  
   Displays the name of the database to be detached.

   **Drop Connections**  
   Disconnect connections to the specified database.

  > [!NOTE]  
  > You can't detach a database with active connections.

   **Update Statistics**  
   By default, the detach operation retains any out-of-date optimization statistics when detaching the database; to update the existing optimization statistics, select this check box.

   **Keep Full-Text Catalogs**  
   By default, the detach operation keeps any full-text catalogs that are associated with the database. To remove them, clear the **Keep Full-Text Catalogs** check box. This option appears only when you're upgrading a database from [!INCLUDE [ssVersion2005](../../../includes/ssversion2005-md.md)].

   **Status**  
   Displays one of the following states: **Ready** or **Not ready**.

   **Message**  
   The **Message** column might display information about the database, as follows:

  - When a database is involved with replication, the **Status** is **Not ready** and the **Message** column displays **Database replicated**.

  - When a database has one or more active connections, the **Status** is **Not ready** and the **Message** column displays _\<number\_of\_active\_connections\>_**Active connection(s)** - for example: **1 Active connection(s)**. Before you can detach the database, you need to disconnect any active connections by selecting **Drop Connections**.

   To obtain more information about a message, select the hyperlinked text to open Activity Monitor.

1. Select **OK**.

1. Using Windows Explorer, move or copy the database files from the source server to the same location on the destination server.

1. Using Windows Explorer, move or copy the backup of the server certificate and the private key file from the source server to the same location on the destination server.

1. Create a database master key on the destination instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. For more information, see **Using Transact-SQL** later in this article.

1. Recreate the server certificate by using the original server certificate backup file. For more information, see **Using Transact-SQL** later in this article.

1. In Object Explorer in [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], right-click the **Databases** folder and select **Attach...**.

1. In the **Attach Databases** dialog box, under **Databases to attach**, select **Add**.

1. In the **Locate Database Files -**_server\_name_ dialog box, select the database file to attach to the new server and select **OK**.

   The following options are available in the **Attach Databases** dialog box.

   **Databases to attach**  
   Displays information about the selected databases.

   \<no column header>  
   Displays an icon indicating the status of the attach operation. The possible icons are described in the **Status** description.

   **MDF File Location**  
   Displays the path and file name of the selected MDF file.

   **Database Name**  
   Displays the name of the database.

   **Attach As**  
   Optionally, specifies a different name for the database to attach as.

   **Owner**  
   Provides a dropdown list of possible database owners from which you can optionally select a different owner.

   **Status**  
   Displays the status of the database according to the following table.

  | Icon | Status text | Description |
  | --- | --- | --- |
  | (No icon) | (No text) | Attach operation wasn't started or might be pending for this object. This is the default when the dialog is opened. |
  | Green, right-pointing triangle | In progress | Attach operation was started but it's not complete. |
  | Green check mark | Success | The object was attached successfully. |
  | Red circle containing a white cross | Error | Attach operation encountered an error and didn't complete successfully. |
  | Circle containing two black quadrants (on left and right) and two white quadrants (on top and bottom) | Stopped | Attach operation wasn't completed successfully because the user stopped the operation. |
  | Circle containing a curved arrow pointing counter-clockwise | Rolled Back | Attach operation was successful but it was rolled back due to an error during attachment of another object. |

   **Message**  
   Displays either a blank message or a "File not found" hyperlink.

   **Add**  
   Find the necessary main database files. When the user selects an .mdf file, applicable information is automatically filled in the respective fields of the **Databases to attach** grid.

   **Remove**  
   Removes the selected file from the **Databases to attach** grid.

   **"** _<database_name>_ **" database details**  
   Displays the names of the files to be attached. To verify or change the pathname of a file, select the **Browse** button (**...**).

  > [!NOTE]  
  > If a file doesn't exist, the **Message** column displays "Not found." If a log file isn't found, it exists in another directory or has been deleted. You need to either update the file path in the **database details** grid to point to the correct location or remove the log file from the grid. If an .ndf data file isn't found, you need to update its path in the grid to point to the correct location.

   **Original File Name**  
   Displays the name of the attached file belonging to the database.

   **File Type**  
   Indicates the type of file, **Data**, or **Log**.

   **Current File Path**  
   Displays the path to the selected database file. The path can be edited manually.

   **Message**  
   Displays either a blank message or a "**File not found**" hyperlink.

### <a id="TsqlMove"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   -- Detach the TDE protected database from the source server.
   USE master;
   GO

   EXEC master.dbo.sp_detach_db @dbname = N'CustRecords';
   GO

   -- Move or copy the database files from the source server to the same location on the destination server.
   -- Move or copy the backup of the server certificate and the private key file from the source server to the same location on the destination server.
   -- Create a database master key on the destination instance of SQL Server.
   USE master;
   GO

   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '*rt@40(FL&dasl1';
   GO

   -- Recreate the server certificate by using the original server certificate backup file.
   -- The password must be the same as the password that was used when the backup was created.
   CREATE CERTIFICATE TestSQLServerCert
   FROM FILE = 'TestSQLServerCert'
   WITH PRIVATE KEY (
       FILE = 'SQLPrivateKeyFile',
       DECRYPTION BY PASSWORD = '*rt@40(FL&dasl1'
   );
   GO

   -- Attach the database that is being moved.
   -- The path of the database files must be the location where you have stored the database files.
   CREATE DATABASE [CustRecords] ON (FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CustRecords.mdf'),
       (FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CustRecords_log.LDF')
   FOR ATTACH;
   GO
   ```

For more information, see:

- [CREATE MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-master-key-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE DATABASE](../../../t-sql/statements/create-database-transact-sql.md)

## Related content

- [Database Detach and Attach (SQL Server)](../../databases/database-detach-and-attach-sql-server.md)
- [Transparent Data Encryption with Azure SQL Database](/azure/azure-sql/database/transparent-data-encryption-tde-overview)
- [sp_detach_db (Transact-SQL)](../../system-stored-procedures/sp-detach-db-transact-sql.md)
