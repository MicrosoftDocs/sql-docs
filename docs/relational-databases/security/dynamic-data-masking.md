---
title: "Dynamic Data Masking"
description: Learn about dynamic data masking, which limits sensitive data exposure by masking it to non-privileged users. It can greatly simplify security in SQL Server.
ms.date: "09/27/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
ms.custom:
- event-tier1-build-2022
author: VanMSFT
ms.author: vanto
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Dynamic Data Masking
[!INCLUDE [SQL Server 2016 ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

![Dynamic data masking](../../relational-databases/security/media/dynamic-data-masking.png)

Dynamic data masking (DDM) limits sensitive data exposure by masking it to non-privileged users. It can be used to greatly simplify the design and coding of security in your application.  

Dynamic data masking helps prevent unauthorized access to sensitive data by enabling customers to specify how much sensitive data to reveal with minimal impact on the application layer. DDM can be configured on designated database fields to hide sensitive data in the result sets of queries. With DDM, the data in the database isn't changed. DDM is easy to use with existing applications, since masking rules are applied in the query results. Many applications can mask sensitive data without modifying existing queries.

* A central data masking policy acts directly on sensitive fields in the database.
* Designate privileged users or roles that do have access to the sensitive data.
* DDM features full masking and partial masking functions, and a random mask for numeric data.
* Simple [!INCLUDE[tsql_md](../../includes/tsql-md.md)] commands define and manage masks.

The purpose of dynamic data masking is to limit exposure of sensitive data, preventing users who shouldn't have access to the data from viewing it. Dynamic data masking doesn't aim to prevent database users from connecting directly to the database and running exhaustive queries that expose pieces of the sensitive data. Dynamic data masking is complementary to other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security features (auditing, encryption, row level security, etc.) and it's highly recommended to use it with them in order to better protect the sensitive data in the database.  
  
Dynamic data masking is available in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)], and is configured by using [!INCLUDE[tsql](../../includes/tsql-md.md)] commands. For more information about configuring dynamic data masking by using the Azure portal, see [Get started with SQL Database Dynamic Data Masking (Azure portal)](/azure/azure-sql/database/dynamic-data-masking-overview).  
  
## Defining a Dynamic Data Mask
 A masking rule may be defined on a column in a table, in order to obfuscate the data in that column. Four types of masks are available.  
  
|Function|Description|Examples|  
|--------------|-----------------|--------------|  
|Default|Full masking according to the data types of the designated fields.<br /><br /> For string data types, use XXXX or fewer Xs if the size of the field is fewer than 4 characters (**char**, **nchar**,  **varchar**, **nvarchar**, **text**, **ntext**).  <br /><br /> For numeric data types use a zero value (**bigint**, **bit**, **decimal**, **int**, **money**, **numeric**, **smallint**, **smallmoney**, **tinyint**, **float**, **real**).<br /><br /> For date and time data types use 01.01.1900 00:00:00.0000000 (**date**, **datetime2**, **datetime**, **datetimeoffset**, **smalldatetime**, **time**).<br /><br />For binary data types use a single byte of ASCII value 0 (**binary**, **varbinary**, **image**).|Example column definition syntax: `Phone# varchar(12) MASKED WITH (FUNCTION = 'default()') NULL`<br /><br /> Example of alter syntax: `ALTER COLUMN Gender ADD MASKED WITH (FUNCTION = 'default()')`|  
|Email|Masking method that exposes the first letter of an email address and the constant suffix ".com", in the form of an email address. `aXXX@XXXX.com`.|Example definition syntax: `Email varchar(100) MASKED WITH (FUNCTION = 'email()') NULL`<br /><br /> Example of alter syntax: `ALTER COLUMN Email ADD MASKED WITH (FUNCTION = 'email()')`|  
|Random|A random masking function for use on any numeric type to mask the original value with a random value within a specified range.|Example definition syntax: `Account_Number bigint MASKED WITH (FUNCTION = 'random([start range], [end range])')`<br /><br /> Example of alter syntax: `ALTER COLUMN [Month] ADD MASKED WITH (FUNCTION = 'random(1, 12)')`|  
|Custom String|Masking method that exposes the first and last letters and adds a custom padding string in the middle. `prefix,[padding],suffix`<br /><br /> Note: If the original value is too short to complete the entire mask, part of the prefix or suffix won't be exposed.|Example definition syntax: `FirstName varchar(100) MASKED WITH (FUNCTION = 'partial(prefix,[padding],suffix)') NULL`<br /><br /> Example of alter syntax: `ALTER COLUMN [Phone Number] ADD MASKED WITH (FUNCTION = 'partial(1,"XXXXXXX",0)')`<br /><br /> Additional example:<br /><br /> `ALTER COLUMN [Phone Number] ADD MASKED WITH (FUNCTION = 'partial(5,"XXXXXXX",0)')`|
|Datetime	| **Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]<br/>Masking method for column defined with data type `datetime`, `datetime2`, `date`, `time`, `datetimeoffset`, `smalldatetime`. It helps masking the `year => datetime("Y")`, `month=> datetime("M")` , `day=>datetime("D")`, `hour=>datetime("h")`, `minute=>datetime("m")`, or `seconds=>datetime("s")` portion of the day. | Example of how to mask the year of the `datetime` value: <br/><br/> `ALTER COLUMN BirthDay ADD MASKED WITH (FUNCTION = 'datetime("Y")')`<br/><br/> Example of how to mask the month of the `datetime` value: <br/><br/> `ALTER COLUMN BirthDay ADD MASKED WITH (FUNCTION = 'datetime("M")')` <br/><br/> Example of how to mask the minute of the `datetime` value: <br/><br/> `ALTER COLUMN BirthDay ADD MASKED WITH (FUNCTION = 'datetime("m")')`|
  
## Permissions  
 You don't need any special permission to create a table with a dynamic data mask, only the standard **CREATE TABLE** and **ALTER** on schema permissions.  
  
 Adding, replacing, or removing the mask of a column, requires the **ALTER ANY MASK** permission and **ALTER** permission on the table. It's appropriate to grant **ALTER ANY MASK** to a security officer.  
  
 Users with **SELECT** permission on a table can view the table data. Columns that are defined as masked, will display the masked data. Grant the **UNMASK** permission to a user to enable them to retrieve unmasked data from the columns for which masking is defined.  
  
 The **CONTROL** permission on the database includes both the **ALTER ANY MASK** and **UNMASK** permission. 
 
  > [!NOTE]  
>  The UNMASK permission does not influence metadata visibility: granting UNMASK alone will not disclose any Metadata. UNMASK will always need to be accompanied by a SELECT permission to have any effect. Example: granting UNMASK on database scope and granting SELECT on an individual Table will have the result that the user can only see the metadata of the individual table from which he can select, not any others. Also see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).
  
## Best practices and common use cases  
  
-   Creating a mask on a column doesn't prevent updates to that column. So although users receive masked data when querying the masked column, the same users can update the data if they have write permissions. A proper access control policy should still be used to limit update permissions.  
  
-   Using `SELECT INTO` or `INSERT INTO` to copy data from a masked column into another table results in masked data in the target table.  
  
-   Dynamic Data Masking is applied when running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export. A database containing masked columns will result in an exported data file with masked data (assuming it's exported by a user without **UNMASK** privileges), and the imported database will contain statically masked data.  
  
## Querying for masked columns  
 Use the **sys.masked_columns** view to query for table-columns that have a masking function applied to them. This view inherits from the **sys.columns** view. It returns all columns in the **sys.columns** view, plus the **is_masked** and **masking_function** columns, indicating if the column is masked, and if so, what masking function is defined. This view only shows the columns on which there's a masking function applied.  
  
```sql 
SELECT c.name, tbl.name as table_name, c.is_masked, c.masking_function  
FROM sys.masked_columns AS c  
JOIN sys.tables AS tbl   
    ON c.[object_id] = tbl.[object_id]  
WHERE is_masked = 1;  
```  
  
## Limitations and restrictions  
 A masking rule can't be defined for the following column types:  
  
-   Encrypted columns (Always Encrypted)  
  
-   FILESTREAM  
  
-   COLUMN_SET or a sparse column that is part of a column set.  
  
-   A mask can't be configured on a computed column, but if the computed column depends on a column with a MASK, then the computed column will return masked data.  
  
-   A column with data masking can't be a key for a FULLTEXT index.  

-   A column in a PolyBase [external table](../../t-sql/statements/create-external-table-transact-sql.md).
  
 For users without the **UNMASK** permission, the deprecated **READTEXT**, **UPDATETEXT**, and **WRITETEXT** statements don't function properly on a column configured for Dynamic Data Masking. 
 
 Adding a dynamic data mask is implemented as a schema change on the underlying table, and therefore can't be performed on a column with dependencies. To work around this restriction, you can first remove the dependency, then add the dynamic data mask and then re-create the dependency. For example, if the dependency is due to an index dependent on that column, you can drop the index, then add the mask, and then re-create the dependent index.
 
Whenever you project an expression referencing a column for which a data masking function is defined, the expression will also be masked. Regardless of the function (default, email, random, custom string) used to mask the referenced column, the resulting expression will always be masked with the default function.

Cross database queries spanning two different Azure SQL databases or databases hosted on different SQL Server Instances, and involve any kind of comparison or join operation on MASKED columns won't provide correct results. The results returned from the remote server are already in MASKED form and not suitable for any kind of comparison or join operation locally.

## Security Note: Bypassing masking using inference or brute-force techniques

Dynamic Data Masking is designed to simplify application development by limiting data exposure in a set of pre-defined queries used by the application. While Dynamic Data Masking can also be useful to prevent accidental exposure of sensitive data when accessing a production database directly, it's important to note that unprivileged users with ad-hoc query permissions can apply techniques to gain access to the actual data. If there's a need to grant such ad-hoc access, Auditing should be used to monitor all database activity and mitigate this scenario.
 
As an example, consider a database principal that has sufficient privileges to run ad-hoc queries on the database, and tries to 'guess' the underlying data and ultimately infer the actual values. Assume that we have a mask defined on the `[Employee].[Salary]` column, and this user connects directly to the database and starts guessing values, eventually inferring the `[Salary]` value of a set of Employees:
 

```sql
SELECT ID, Name, Salary FROM Employees
WHERE Salary > 99999 and Salary < 100001;
```

>    |  Id | Name| Salary |   
>    | ----- | ---------- | ------ | 
>    |  62543 | Jane Doe | 0 | 
>    |  91245 | John Smith | 0 |  

This demonstrates that Dynamic Data Masking shouldn't be used as an isolated measure to fully secure sensitive data from users running ad-hoc queries on the database. It's appropriate for preventing accidental sensitive data exposure, but won't protect against malicious intent to infer the underlying data.
 
It's important to properly manage the permissions on the database, and to always follow the minimal required permissions principle. Also, remember to have Auditing enabled to track all activities taking place on the database.

## <a id="granular"></a> Granular permissions introduced in SQL Server 2022

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can prevent unauthorized access to sensitive data and gain control by masking it to an unauthorized user at different levels of the database. You can grant or revoke **UNMASK** permission at the database-level, schema-level, table-level or at the column-level to a user. This enhancement provides a more granular way to control and limit unauthorized access to data stored in the database and improve data security management.
  
## Examples  
  
### Creating a Dynamic Data Mask  
 The following example creates a table with three different types of dynamic data masks. The example populates the table, and selects to show the result.  
  
```sql

-- schema to contain user tables
CREATE SCHEMA Data;
GO

-- table with masked columns
CREATE TABLE Data.Membership(
    MemberID        int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
    FirstName        varchar(100) MASKED WITH (FUNCTION = 'partial(1, "xxxxx", 1)') NULL,
    LastName        varchar(100) NOT NULL,
    Phone            varchar(12) MASKED WITH (FUNCTION = 'default()') NULL,
    Email            varchar(100) MASKED WITH (FUNCTION = 'email()') NOT NULL,
    DiscountCode    smallint MASKED WITH (FUNCTION = 'random(1, 100)') NULL
    );

-- inserting sample data
INSERT INTO Data.Membership (FirstName, LastName, Phone, Email, DiscountCode)
VALUES   
('Roberto', 'Tamburello', '555.123.4567', 'RTamburello@contoso.com', 10),  
('Janice', 'Galvin', '555.123.4568', 'JGalvin@contoso.com.co', 5),  
('Shakti', 'Menon', '555.123.4570', 'SMenon@contoso.net', 50),  
('Zheng', 'Mu', '555.123.4569', 'ZMu@contoso.net', 40);  

```  
  
 A new user is created and granted the **SELECT** permission on the schema where the table resides. Queries executed as the `MaskingTestUser` view masked data.  
  
```sql 
CREATE USER MaskingTestUser WITHOUT LOGIN;  

GRANT SELECT ON SCHEMA::Data TO MaskingTestUser;  
  
  -- impersonate for testing:
EXECUTE AS USER = 'MaskingTestUser';  

SELECT * FROM Data.Membership;  

REVERT;  
```  
  
 The result demonstrates the masks by changing the data from  
  
 `1    Roberto     Tamburello    555.123.4567    RTamburello@contoso.com    10`  
  
 into  
  
 `1    Rxxxxxo    Tamburello    xxxx            RXXX@XXXX.com            91`
 
 where the number in DiscountCode is random for every query result.
  
### Adding or editing a mask on an existing column
 Use the **ALTER TABLE** statement to add a mask to an existing column in the table, or to edit the mask on that column.  
The following example adds a masking function to the `LastName` column:  

```sql  
ALTER TABLE Data.Membership  
ALTER COLUMN LastName ADD MASKED WITH (FUNCTION = 'partial(2,"xxxx",0)');  
```  
  
 The following example changes a masking function on the `LastName` column:  

```sql  
ALTER TABLE Data.Membership  
ALTER COLUMN LastName varchar(100) MASKED WITH (FUNCTION = 'default()');  
```  
  
### Granting permissions to view unmasked data  
 Granting the **UNMASK** permission allows `MaskingTestUser` to see the data unmasked.  
  
```sql
GRANT UNMASK TO MaskingTestUser;  

EXECUTE AS USER = 'MaskingTestUser';  

SELECT * FROM Data.Membership;  

REVERT;    
  
-- Removing the UNMASK permission  
REVOKE UNMASK TO MaskingTestUser;  
```  
  
### Dropping a Dynamic Data Mask  
 The following statement drops the mask on the `LastName` column created in the previous example:  
  
```sql  
ALTER TABLE Data.Membership   
ALTER COLUMN LastName DROP MASKED;  
```  

### Granular permission examples

1. Create schema to contain user tables

   ```sql
   CREATE SCHEMA Data; 
   GO 
   ```

1. Create table with masked columns

   ```sql
   CREATE TABLE Data.Membership (
     MemberID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
     FirstName varchar(100) MASKED WITH (FUNCTION = 'partial(1, "xxxxx", 1)') NULL,
     LastName varchar(100) NOT NULL,
     Phone varchar(12) MASKED WITH (FUNCTION = 'default()') NULL,
     Email varchar(100) MASKED WITH (FUNCTION = 'email()') NOT NULL,
     DiscountCode smallint MASKED WITH (FUNCTION = 'random(1, 100)') NULL,
     BirthDay datetime MASKED WITH (FUNCTION = 'default()') NULL
   ); 
   ```

1. Insert sample data

   ```sql
   INSERT INTO Data.Membership (FirstName, LastName, Phone, Email, DiscountCode, BirthDay) 
   VALUES    
   ('Roberto', 'Tamburello', '555.123.4567', 'RTamburello@contoso.com', 10, '1985-01-25 03:25:05'),   
   ('Janice', 'Galvin', '555.123.4568', 'JGalvin@contoso.com.co', 5,'1990-05-14 11:30:00'),   
   ('Shakti', 'Menon', '555.123.4570', 'SMenon@contoso.net', 50,'2004-02-29 14:20:10'),   
   ('Zheng', 'Mu', '555.123.4569', 'ZMu@contoso.net', 40,'1990-03-01 06:00:00'); 
   ```

1. Create schema to contain service tables

   ```sql
   CREATE SCHEMA Service; 
   GO 
   ```

1. Create service table with masked columns

   ```sql
   CREATE TABLE Service.Feedback ( 
       MemberID int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED, 
       Feedback varchar(100) MASKED WITH (FUNCTION = 'default()') NULL, 
       Rating int MASKED WITH (FUNCTION='default()'), 
       Received_On datetime 
       );
   ```

1. Insert sample data

   ```sql
   INSERT INTO Service.Feedback(Feedback,Rating,Received_On)
   VALUES  
   ('Good',4,'2022-01-25 11:25:05'),   
   ('Excellent', 5, '2021-12-22 08:10:07'),   
   ('Average', 3, '2021-09-15 09:00:00'); 
   ```

1. Create different users in the database

   ```sql
   CREATE USER ServiceAttendant WITHOUT LOGIN;  
   GO
   
   CREATE USER ServiceLead WITHOUT LOGIN;  
   GO
   
   CREATE USER ServiceManager WITHOUT LOGIN;  
   GO  
   
   CREATE USER ServiceHead WITHOUT LOGIN;  
   GO
   ```

1. Grant read permissions to the users in the database 

   ```sql
   ALTER ROLE db_datareader ADD MEMBER ServiceAttendant; 
   
   ALTER ROLE db_datareader ADD MEMBER ServiceLead; 
   
   ALTER ROLE db_datareader ADD MEMBER ServiceManager; 
   
   ALTER ROLE db_datareader ADD MEMBER ServiceHead; 
   ```

1. Grant different UNMASK permissions to users

   ```sql
   --Grant column level UNMASK permission to ServiceAttendant  
   GRANT UNMASK ON Data.Membership(FirstName) TO ServiceAttendant;  
   
   -- Grant table level UNMASK permission to ServiceLead  
   GRANT UNMASK ON Data.Membership TO ServiceLead;  
   
   -- Grant schema level UNMASK permission to ServiceManager  
   GRANT UNMASK ON SCHEMA::Data TO ServiceManager;  
   GRANT UNMASK ON SCHEMA::Service TO ServiceManager;  
   
   --Grant database level UNMASK permission to ServiceHead;  
   GRANT UNMASK TO ServiceHead;
   ```

1. Query the data under the context of user `ServiceAttendant`

   ```sql
   EXECUTE AS USER='ServiceAttendant';  
   SELECT MemberID,FirstName,LastName,Phone,Email,BirthDay FROM Data.   Membership;  
   SELECT MemberID,Feedback,Rating FROM Service.Feedback;  
   REVERT; 
   ```

1. Query the data under the context of user `ServiceLead`  

   ```sql
   EXECUTE AS USER='ServiceLead';  
   SELECT MemberID,FirstName,LastName,Phone,Email,BirthDay FROM Data.   Membership;  
   SELECT MemberID,Feedback,Rating FROM Service.Feedback;  
   REVERT; 
   ```
 
1. Query the data under the context of user `ServiceManager`  

   ```sql
   EXECUTE AS USER='ServiceManager';  
   SELECT MemberID,FirstName,LastName,Phone,Email FROM Data.Membership;  
   SELECT MemberID,Feedback,Rating FROM Service.Feedback;  
   REVERT; 
   ```
 
1. Query the data under the context of user `ServiceHead` 

   ```sql
   EXECUTE AS USER='ServiceHead';  
   SELECT MemberID,FirstName,LastName,Phone,Email,BirthDay FROM Data.Membership;  
   SELECT MemberID,Feedback,Rating FROM Service.Feedback;  
   REVERT;  
   ```
 

1. To revoke UNMASK permissions, use the following T-SQL statements:

   ```sql
   REVOKE UNMASK ON Data.Membership(FirstName) FROM ServiceAttendant; 
   
   REVOKE UNMASK ON Data.Membership FROM ServiceLead; 
   
   REVOKE UNMASK ON SCHEMA::Data FROM ServiceManager; 
   
   REVOKE UNMASK ON SCHEMA::Service FROM ServiceManager; 
   
   REVOKE UNMASK FROM ServiceHead; 
   ```

## See also  
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [column_definition &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-column-definition-transact-sql.md)   
 [sys.masked_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-masked-columns-transact-sql.md)   
 [Get started with SQL Database Dynamic Data Masking (Azure portal)](/azure/azure-sql/database/dynamic-data-masking-overview)
