---
title: "bcp Utility | Microsoft Docs"
ms.custom: ""
ms.date: "10/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "bcp utility [SQL Server]"
  - "exporting data"
  - "row exporting [SQL Server]"
  - "copying data [SQL Server], bcp utility"
  - "command prompt utilities [SQL Server], bcp"
  - "tables [SQL Server], importing data"
  - "column importing [SQL Server]"
  - "bcp utility [SQL Server], command options"
  - "file exporting [SQL Server]"
  - "bulk copy [SQL Server]"
  - "bcp utility [SQL Server], about bcp utility"
  - "tables [SQL Server], exporting data"
  - "row importing [SQL Server]"
  - "importing data, bcp utility"
  - "file importing [SQL Server]"
  - "column exporting [SQL Server]"
ms.assetid: c0af54f5-ca4a-4995-a3a4-0ce39c30ec38
author: stevestein
ms.author: sstein
manager: craigg
---
# bcp Utility
  The **bcp** utility bulk copies data between an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and a data file in a user-specified format. The **bcp** utility can be used to import large numbers of new rows into [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tables or to export data out of tables into data files. Except when used with the **queryout** option, the utility requires no knowledge of [!INCLUDE[tsql](../includes/tsql-md.md)]. To import data into a table, you must either use a format file created for that table or understand the structure of the table and the types of data that are valid for its columns.  
  
 ![Topic link icon](../../2014/database-engine/media/topic-link.gif "Topic link icon") For the syntax conventions that are used for the **bcp** syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/transact-sql-syntax-conventions-transact-sql).  
  
> [!NOTE]  
>  If you use **bcp** to back up your data, create a format file to record the data format. **bcp** data files do not include any schema or format information, so if a table or view is dropped and you do not have a format file, you may be unable to import the data.  
  
## Syntax  
  
```  
  
   bcp [database_name.] schema.{table_name | view_name | "query" {indata_file | outdata_file | queryoutdata_file | format nul}  
  
[-apacket_size]  
[-bbatch_size]  
[-c]  
[-C { ACP | OEM | RAW | code_page } ]  
[-ddatabase_name]  
[-eerr_file]  
[-E]  
[-fformat_file]  
[-Ffirst_row]  
[-h"hint [,...n]"]   
[-iinput_file]  
[-k]  
[-Kapplication_intent]  
[-Llast_row]  
[-mmax_errors]  
[-n]  
[-N]  
[-ooutput_file]  
[-Ppassword]  
[-q]  
[-rrow_term]  
[-R]  
[-S [server_name[\instance_name]]  
[-tfield_term]  
[-T]  
[-Ulogin_id]  
[-v]  
[-V (80 | 90 | 100 | 110)]  
[-w]  
[-x]  
/?  
```  
  
## Arguments  
 *data_file*  
 Is the full path of the data file. When data is bulk imported into [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the data file contains the data to be copied into the specified table or view. When data is bulk exported from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the data file contains the data copied from the table or view. The path can have from 1 through 255 characters. The data file can contain a maximum of 2<sup>63</sup> - 1 rows.  
  
 *database_name*  
 Is the name of the database in which the specified table or view resides. If not specified, this is the default database for the user.  
  
 You can also explicitly specify the database name with `d-`.  
  
 **in** _data_file_ | **out**_data_file_ | **queryout**_data_file_ | **format nul**  
 Specifies the direction of the bulk copy, as follows:  
  
-   **in** copies from a file into the database table or view.  
  
-   **out** copies from the database table or view to a file. If you specify an existing file, the file is overwritten. When extracting data, note that the **bcp** utility represents an empty string as a null and a null string as an empty string.  
  
-   **queryout** copies from a query and must be specified only when bulk copying data from a query.  
  
-   **format** creates a format file based on the option specified (**-n**, `-c`, `-w`, or **-N**) and the table or view delimiters. When bulk copying data, the **bcp** command can refer to a format file, which saves you from re-entering format information interactively. The **format** option requires the **-f** option; creating an XML format file, also requires the **-x** option. For more information, see [Create a Format File &#40;SQL Server&#41;](../relational-databases/import-export/create-a-format-file-sql-server.md). You must specify **nul** as the value (**format nul**).  
  
 *owner*  
 Is the name of the owner of the table or view. *owner* is optional if the user performing the operation owns the specified table or view. If *owner* is not specified and the user performing the operation does not own the specified table or view, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] returns an error message, and the operation is canceled.  
  
 **"** _query_ **"**  
 Is a [!INCLUDE[tsql](../includes/tsql-md.md)] query that returns a result set. If the query returns multiple result sets, only the first result set is copied to the data file; subsequent result sets are ignored. Use double quotation marks around the query and single quotation marks around anything embedded in the query. **queryout** must also be specified when bulk copying data from a query.  
  
 The query can reference a stored procedure as long as all tables referenced inside the stored procedure exist prior to executing the bcp statement. For example, if the stored procedure generates a temp table, the **bcp** statement fails because the temp table is available only at run time and not at statement execution time. In this case, consider inserting the results of the stored procedure into a table and then use **bcp** to copy the data from the table into a data file.  
  
 *table_name*  
 Is the name of the destination table when importing data into [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (**in**), and the source table when exporting data from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (**out**).  
  
 *view_name*  
 Is the name of the destination view when copying data into [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (**in**), and the source view when copying data from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (**out**). Only views in which all columns refer to the same table can be used as destination views. For more information on the restrictions for copying data into views, see [INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/insert-transact-sql).  
  
 **-a** _packet_size_  
 Specifies the number of bytes, per network packet, sent to and from the server. A server configuration option can be set by using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] (or the **sp_configure** system stored procedure). However, the server configuration option can be overridden on an individual basis by using this option. *packet_size* can be from 4096 to 65535 bytes; the default is 4096.  
  
 Increased packet size can enhance performance of bulk-copy operations. If a larger packet is requested but cannot be granted, the default is used. The performance statistics generated by the **bcp** utility show the packet size used.  
  
 **-b** _batch_size_  
 Specifies the number of rows per batch of imported data. Each batch is imported and logged as a separate transaction that imports the whole batch before being committed. By default, all the rows in the data file are imported as one batch. To distribute the rows among multiple batches, specify a *batch_size* that is smaller than the number of rows in the data file. If the transaction for any batch fails, only insertions from the current batch are rolled back. Batches already imported by committed transactions are unaffected by a later failure.  
  
 Do not use this option in conjunction with the **-h"**ROWS_PER_BATCH **=*`bb`*"** option.  
  
 `-c`  
 Performs the operation using a character data type. This option does not prompt for each field; it uses `char` as the storage type, without prefixes and with **\t** (tab character) as the field separator and **\r\n** (newline character) as the row terminator. `-c` is not compatible with `-w`.  
  
 For more information, see [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md).  
  
 **-C** { **ACP** | **OEM** | **RAW** | *code_page* }  
 Specifies the code page of the data in the data file. *code_page* is relevant only if the data contains `char`, `varchar`, or `text` columns with character values greater than 127 or less than 32.  
  
> [!NOTE]  
>  We recommend specifying a collation name for each column in a format file.  
  
|Code page value|Description|  
|---------------------|-----------------|  
|ACP|[!INCLUDE[vcpransi](../includes/vcpransi-md.md)]/Microsoft Windows (ISO 1252).|  
|OEM|Default code page used by the client. This is the default code page used if **-C** is not specified.|  
|RAW|No conversion from one code page to another occurs. This is the fastest option because no conversion occurs.|  
|*code_page*|Specific code page number; for example, 850.<br /><br /> **&#42;&#42; Important &#42;&#42;** [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not support code page 65001 (UTF-8 encoding).|  
  
 `-d` *database_name*  
 Specifies the database to connect to. By default, bcp.exe connects to the user's default database. If `-d`*database_name* and a three part name (*database_name.schema.table*, passed as the first parameter to bcp.exe) is specified, an error will occur because you cannot specify the database name twice.If *database_name* begins with a hyphen (-) or a forward slash (/), do not add a space between `-d` and the database name.  
  
 **-e** _err_file_  
 Specifies the full path of an error file used to store any rows that the **bcp** utility cannot transfer from the file to the database. Error messages from the **bcp** command go to the workstation of the user. If this option is not used, an error file is not created.  
  
 If *err_file* begins with a hyphen (-) or a forward slash (/), do not include a space between **-e** and the *err_file* value.  
  
 **-E**  
 Specifies that identity value or values in the imported data file are to be used for the identity column. If **-E** is not given, the identity values for this column in the data file being imported are ignored, and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] automatically assigns unique values based on the seed and increment values specified during table creation.  
  
 If the data file does not contain values for the identity column in the table or view, use a format file to specify that the identity column in the table or view should be skipped when importing data; [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] automatically assigns unique values for the column. For more information, see [DBCC CHECKIDENT &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-checkident-transact-sql).  
  
 The **-E** option has a special permissions requirement. For more information, see "Remarks" later in this topic.  
  
 **-f** _format_file_  
 Specifies the full path of a format file. The meaning of this option depends on the environment in which it is used, as follows:  
  
-   If **-f** is used with the **format** option, the specified *format_file* is created for the specified table or view. To create an XML format file, also specify the **-x** option. For more information, see [Create a Format File &#40;SQL Server&#41;](../relational-databases/import-export/create-a-format-file-sql-server.md).  
  
-   If used with the **in** or **out** option, **-f** requires an existing format file.  
  
    > [!NOTE]  
    >  Using a format file in with the **in** or **out** option is optional. In the absence of the **-f** option, if **-n**, `-c`, `-w`, or **-N** is not specified, the command prompts for format information and lets you save your responses in a format file (whose default file name is Bcp.fmt).  
  
 If *format_file* begins with a hyphen (-) or a forward slash (/), do not include a space between **-f** and the *format_file* value.  
  
 **-F** _first_row_  
 Specifies the number of the first row to export from a table or import from a data file. This parameter requires a value greater than (>) 0 but less than (\<) or equal to (=) the total number rows. In the absence of this parameter, the default is the first row of the file.  
  
 *first_row* can be a positive integer with a value up to 2^63-1. **-F**_first_row_ is 1-based.  
  
 **-h"** _hint_[ **,**... *n*] **"**  
 Specifies the hint or hints to be used during a bulk import of data into a table or view.  
  
 ORDER**(**_column_[ASC | DESC] [**,**...*n*]**)**  
 The sort order of the data in the data file. Bulk import performance is improved if the data being imported is sorted according to the clustered index on the table, if any. If the data file is sorted in a different order, that is other than the order of a clustered index key, or if there is no clustered index on the table, the ORDER clause is ignored. The column names supplied must be valid column names in the destination table. By default, **bcp** assumes the data file is unordered. For optimized bulk import, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] also validates that the imported data is sorted.  
  
 ROWS_PER_BATCH **=**_bb_  
 Number of rows of data per batch (as *bb*). Used when **-b** is not specified, resulting in the entire data file being sent to the server as a single transaction. The server optimizes the bulk load according to the value *bb*. By default, ROWS_PER_BATCH is unknown.  
  
 KILOBYTES_PER_BATCH **=** _cc_  
 Approximate number of kilobytes of data per batch (as *cc*). By default, KILOBYTES_PER_BATCH is unknown.  
  
 TABLOCK  
 Specifies that a bulk update table-level lock is acquired for the duration of the bulk load operation; otherwise, a row-level lock is acquired. This hint significantly improves performance because holding a lock for the duration of the bulk-copy operation reduces lock contention on the table. A table can be loaded concurrently by multiple clients if the table has no indexes and **TABLOCK** is specified. By default, locking behavior is determined by the table option **table lock on bulk load**.  
  
 CHECK_CONSTRAINTS  
 Specifies that all constraints on the target table or view must be checked during the bulk-import operation. Without the CHECK_CONSTRAINTS hint, any CHECK and FOREIGN KEY constraints are ignored, and after the operation the constraint on the table is marked as not-trusted.  
  
> [!NOTE]  
>  UNIQUE, PRIMARY KEY, and NOT NULL constraints are always enforced.  
  
 At some point, you will need to check the constraints on the entire table. If the table was nonempty before the bulk import operation, the cost of revalidating the constraint may exceed the cost of applying CHECK constraints to the incremental data. Therefore, we recommend that normally you enable constraint checking during an incremental bulk import.  
  
 A situation in which you might want constraints disabled (the default behavior) is if the input data contains rows that violate constraints. With CHECK constraints disabled, you can import the data and then use [!INCLUDE[tsql](../includes/tsql-md.md)] statements to remove data that is not valid.  
  
> [!NOTE]  
>  **bcp** now enforces data validation and data checks that might cause scripts to fail if they are executed on invalid data in a data file.  
  
> [!NOTE]  
>  The **-m** _max_errors_ switch does not apply to constraint checking.  
  
 FIRE_TRIGGERS  
 Specified with the **in** argument, any insert triggers defined on the destination table will run during the bulk-copy operation. If FIRE_TRIGGERS is not specified, no insert triggers will run. FIRE_TRIGGERS is ignored for the **out**, **queryout**, and **format** arguments.  
  
 **-i** _input_file_  
 Specifies the name of a response file, containing the responses to the command prompt questions for each data field when a bulk copy is being performed using interactive mode (**-n**, `-c`, `-w`, or **-N** not specified).  
  
 If *input_file* begins with a hyphen (-) or a forward slash (/), do not include a space between **-i** and the *input_file* value.  
  
 **-k**  
 Specifies that empty columns should retain a null value during the operation, rather than have any default values for the columns inserted. For more information, see [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md).  
  
 **-K** _application_intent_  
 Declares the application workload type when connecting to a server. The only value that is possible is **ReadOnly**. If **-K** is not specified, the bcp utility will not support connectivity to a secondary replica in an AlwaysOn availability group. For more information, see [ Active Secondaries: Readable Secondary Replicas (AlwaysOn Availability Groups)](../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).  
  
 **-L** _last_row_  
 Specifies the number of the last row to export from a table or import from a data file. This parameter requires a value greater than (>) 0 but less than (\<) or equal to (=) the number of the last row. In the absence of this parameter, the default is the last row of the file.  
  
 *last_row* can be a positive integer with a value up to 2^63-1.  
  
 **-m** _max_errors_  
 Specifies the maximum number of syntax errors that can occur before the **bcp** operation is canceled. A syntax error implies a data conversion error to the target data type. The *max_errors* total excludes any errors that can be detected only at the server, such as constraint violations.  
  
 A row that cannot be copied by the **bcp** utility is ignored and is counted as one error. If this option is not included, the default is 10.  
  
> [!NOTE]  
>  The **-m** option also does not apply to converting the `money` or `bigint` data types.  
  
 **-n**  
 Performs the bulk-copy operation using the native (database) data types of the data. This option does not prompt for each field; it uses the native values.  
  
 For more information, see [Use Native Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md).  
  
 **-N**  
 Performs the bulk-copy operation using the native (database) data types of the data for noncharacter data, and Unicode characters for character data. This option offers a higher performance alternative to the `-w` option, and is intended for transferring data from one instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to another using a data file. It does not prompt for each field. Use this option when you are transferring data that contains ANSI extended characters and you want to take advantage of the performance of native mode.  
  
 For more information, see [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md).  
  
 If you export and then import data to the same table schema by using bcp.exe with **-N**, you might see a truncation warning if there is a fixed length, non-Unicode character column (for example, `char(10)`).  
  
 The warning can be ignored. One way to resolve this warning is to use **-n** instead of **-N**.  
  
 **-o** _output_file_  
 Specifies the name of a file that receives output redirected from the command prompt.  
  
 If *output_file* begins with a hyphen (-) or a forward slash (/), do not include a space between **-o** and the *output_file* value.  
  
 **-P** _password_  
 Specifies the password for the login ID. If this option is not used, the **bcp** command prompts for a password. If this option is used at the end of the command prompt without a password, **bcp** uses the default password (NULL).  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../includes/ssnotestrongpass-md.md)]  
  
 To mask your password, do not specify the **-P** option along with the **-U** option. Instead, after specifying **bcp** along with the **-U** option and other switches (do not specify **-P**), press ENTER, and the command will prompt you for a password. This method ensures that your password will be masked when it is entered.  
  
 If *password* begins with a hyphen (-) or a forward slash (/), do not add a space between **-P** and the *password* value.  
  
 `-q`  
 Executes the SET QUOTED_IDENTIFIERS ON statement in the connection between the **bcp** utility and an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Use this option to specify a database, owner, table, or view name that contains a space or a single quotation mark. Enclose the entire three-part table or view name in quotation marks ("").  
  
 To specify a database name that contains a space or single quotation mark, you must use the **-q** option.  
  
 `-q` does not apply to values passed to `-d`.  
  
 For more information, see Remarks, later in this topic.  
  
 **-r** _row_term_  
 Specifies the row terminator. The default is **\n** (newline character). Use this parameter to override the default row terminator. For more information, see [Specify Field and Row Terminators &#40;SQL Server&#41;](../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).  
  
 If you specify the row terminator in hexadecimal notation in a bcp.exe command, the value will be truncated at 0x00. For example, if you specify 0x410041, 0x41 will be used.  
  
 If *row_term* begins with a hyphen (-) or a forward slash (/), do not include a space between **-r** and the *row_term* value.  
  
 **-R**  
 Specifies that currency, date, and time data is bulk copied into [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using the regional format defined for the locale setting of the client computer. By default, regional settings are ignored.  
  
 **-S** _server_name_[ **\\**_instance_name_]  
 Specifies the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to which to connect. If no server is specified, the **bcp** utility connects to the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on the local computer. This option is required when a **bcp** command is run from a remote computer on the network or a local named instance. To connect to the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a server, specify only *server_name*. To connect to a named instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], specify *server_name**_\\_**instance_name*.  
  
 `-t` *field_term*  
 Specifies the field terminator. The default is **\t** (tab character). Use this parameter to override the default field terminator. For more information, see [Specify Field and Row Terminators &#40;SQL Server&#41;](../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md).  
  
 If you specify the field terminator in hexadecimal notation in a bcp.exe command, the value will be truncated at 0x00. For example, if you specify 0x410041, 0x41 will be used.  
  
 If *field_term* begins with a hyphen (-) or a forward slash (/), do not include a space between `-t` and the *field_term* value.  
  
 **-T**  
 Specifies that the **bcp** utility connects to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with a trusted connection using integrated security. The security credentials of the network user, *login_id*, and *password* are not required. If **-T** is not specified, you need to specify **-U** and **-P** to successfully log in.  
  
 **-U** _login_id_  
 Specifies the login ID used to connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  When the **bcp** utility is connecting to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with a trusted connection using integrated security, use the **-T** option (trusted connection) instead of the *user name* and *password* combination.  
  
 **-v**  
 Reports the **bcp** utility version number and copyright.  
  
 **-V** (**80** | **90** | **100**| **110**)  
 Performs the bulk-copy operation using data types from an earlier version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. This option does not prompt for each field; it uses the default values.  
  
 **80** = [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)]  
  
 **90** = [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)]  
  
 **100** = [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)]  
  
 **110 = [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]**  
  
 For example, to generate data for types not supported by [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)], but were introduced in later versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], use the -V80 option.  
  
 For more information, see [Import Native and Character Format Data from Earlier Versions of SQL Server](../relational-databases/import-export/import-native-and-character-format-data-from-earlier-versions-of-sql-server.md).  
  
 `-w`  
 Performs the bulk copy operation using Unicode characters. This option does not prompt for each field; it uses `nchar` as the storage type, no prefixes, **\t** (tab character) as the field separator, and **\n** (newline character) as the row terminator. `-w` is not compatible with `-c`.  
  
 For more information, see [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md).  
  
 **-x**  
 Used with the **format** and **-f**_format_file_ options, generates an XML-based format file instead of the default non-XML format file. The **-x** does not work when importing or exporting data. It generates an error if used without both **format** and **-f**_format_file_.  
  
## Remarks  
 The **bcp** 12.0 client is installed when you install [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] tools. If tools are installed for both [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] and an earlier version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], depending on the value of the PATH environment variable, you might be using the earlier **bcp** client instead of the **bcp** 12.0 client. This environment variable defines the set of directories used by Windows to search for executable files. To discover which version you are using, run the **bcp /v** command at the Windows Command Prompt. For information about how to set the command path in the PATH environment variable, see Windows Help.  
  
 XML format files are only supported when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools are installed together with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Native Client.  
  
 For information about where to find or how to run the **bcp** utility and about the command prompt utilities syntax conventions, see [Command Prompt Utility Reference &#40;Database Engine&#41;](../tools/command-prompt-utility-reference-database-engine.md).  
  
 For information on preparing data for bulk import or export operations, see [Prepare Data for Bulk Export or Import &#40;SQL Server&#41;](../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md).  
  
 For information about when row-insert operations that are performed by bulk import are logged in the transaction log, see [Prerequisites for Minimal Logging in Bulk Import](../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).  
  
## Native Data File Support  
 In [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], the **bcp** utility supports native data files compatible with [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)], [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)], and [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
## Computed Columns and timestamp Columns  
 Values in the data file being imported for computed or `timestamp` columns are ignored, and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] automatically assigns values. If the data file does not contain values for the computed or `timestamp` columns in the table, use a format file to specify that the computed or `timestamp` columns in the table should be skipped when importing data; [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] automatically assigns values for the column.  
  
 Computed and `timestamp` columns are bulk copied from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to a data file as usual.  
  
## Specifying Identifiers That Contain Spaces or Quotation Marks  
 [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] identifiers can include characters such as embedded spaces and quotation marks. Such identifiers must be treated as follows:  
  
-   When you specify an identifier or file name that includes a space or quotation mark at the command prompt, enclose the identifier in quotation marks ("").  
  
     For example, the following `bcp out` command creates a data file named `Currency Types.dat`:  
  
    ```  
    bcp AdventureWorks2012.Sales.Currency out "Currency Types.dat" -T -c  
    ```  
  
-   To specify a database name that contains a space or quotation mark, you must use the `-q` option.  
  
-   For owner, table, or view names that contain embedded spaces or quotation marks, you can either:  
  
    -   Specify the `-q` option, or  
  
    -   Enclose the owner, table, or view name in brackets ([]) inside the quotation marks.  
  
## Data Validation  
 **bcp** now enforces data validation and data checks that might cause scripts to fail if they are executed on invalid data in a data file. For example, **bcp** now verifies that:  
  
-   The native representation of `float` or `real` data types are valid.  
  
-   Unicode data has an even-byte length.  
  
 Forms of invalid data that could be bulk imported in earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] might fail to load now; whereas, in earlier versions, the failure did not occur until a client tried to access the invalid data. The added validation minimizes surprises when querying the data after bulk load.  
  
## Bulk Exporting or Importing SQLXML Documents  
 To bulk export or import SQLXML data, use one of the following data types in your format file.  
  
|Data type|Effect|  
|---------------|------------|  
|SQLCHAR or SQLVARYCHAR|The data is sent in the client code page or in the code page implied by the collation). The effect is the same as specifying the `-c` switch without specifying a format file.|  
|SQLNCHAR or SQLNVARCHAR|The data is sent as Unicode. The effect is the same as specifying the `-w` switch without specifying a format file.|  
|SQLBINARY or SQLVARYBIN|The data is sent without any conversion.|  
  
## Permissions  
 A **bcpout** operation requires SELECT permission on the source table.  
  
 A **bcpin** operation minimally requires SELECT/INSERT permissions on the target table. In addition, ALTER TABLE permission is required if any of the following is true:  
  
-   Constraints exist and the CHECK_CONSTRAINTS hint is not specified.  
  
    > [!NOTE]  
    >  Disabling constraints is the default behavior. To enable constraints explicitly, use the **-h** option with the CHECK_CONSTRAINTS hint.  
  
-   Triggers exist and the FIRE_TRIGGER hint is not specified.  
  
    > [!NOTE]  
    >  By default, triggers are not fired. To fire triggers explicitly, use the **-h** option with the FIRE_TRIGGERS hint.  
  
-   You use the **-E** option to import identity values from a data file.  
  
> [!NOTE]  
>  Requiring ALTER TABLE permission on the target table was new in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)]. This new requirement might cause **bcp** scripts that do not enforce triggers and constraint checks to fail if the user account lacks ALTER table permissions for the target table.  
  
## Character Mode (-c) and Native Mode (-n) Best Practices  
 This section has recommendations for to character mode (-c) and native mode (-n).  
  
-   (Administrator/User) When possible, use native format (-n) to avoid the separator issue. Use the native format to export and import using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Export data from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using the -c or -w option if the data will be imported to a non-[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database.  
  
-   (Administrator) Verify data when using BCP OUT. For example, when you use BCP OUT, BCP IN, and then BCP OUT verify that the data is properly exported and the terminator values are not used as part of some data value. Please consider overriding the default terminators (using -t and -r options) with random hexadecimal values to avoid conflicts between terminator values and data values.  
  
-   (User) Use a long and unique terminator (any sequence of bytes or characters) to minimize the possibility of a conflict with the actual string value. This can be done by using the -t and -r options.  
  
## Examples  
 This section contains the following examples:  
  
-   A. Copying table rows into a data file (with a trusted connection)  
  
-   B. Copying table rows into a data file (with Mixed-mode Authentication)  
  
-   C. Copying data from a file to a table  
  
-   D. Copying a specific column into a data file  
  
-   E. Copying a specific row into a data file  
  
-   F. Copying data from a query to a data file  
  
-   G. Creating a non-XML format file  
  
-   H. Creating an XML format file  
  
-   I. Using a format file to bulk import with **bcp**  
  
### A. Copying table rows into a data file (with a trusted connection)  
 The following example illustrates the **out** option on the `AdventureWorks2012.Sales.Currency` table. This example creates a data file named `Currency.dat` and copies the table data into it using character format. The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 At a command prompt, enter the following command:  
  
```  
bcp AdventureWorks2012.Sales.Currency out Currency.dat -T -c  
```  
  
### B. Copying table rows into a data file (with mixed-mode authentication)  
 The following example illustrates the **out** option on the `AdventureWorks2012.Sales.Currency` table. This example creates a data file named `Currency.dat` and copies the table data into it using character format.  
  
 The example assumes that you are using mixed-mode authentication, you must use the **-U** switch to specify your login ID. Also, unless you are connecting to the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on the local computer, use the **-S** switch to specify the system name and, optionally, an instance name.  
  
```  
bcp AdventureWorks2012.Sales.Currency out Currency.dat -c -U<login_id> -S<server_name\instance_name>  
```  
  
 The system will prompt you for your password.  
  
### C. Copying data from a file to a table  
 The following example illustrates the **in** option by using the file created in the preceding example (`Currency.dat`). First, however, this example creates an empty copy of the `AdventureWorks2012 Sales.Currency` table, `Sales.Currency2`, into which the data is copied. The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 To create the empty table, in Query Editor, enter the following command:  
  
```  
USE AdventureWorks2012;  
GO  
SELECT * INTO AdventureWorks2012.Sales.Currency2   
FROM AdventureWorks2012.Sales.Currency WHERE 1=2;  
```  
  
 To bulk copy the character data into the new table, that is to import the data, enter the following command at a command prompt:  
  
```  
bcp AdventureWorks2012.Sales.Currency2 in Currency.dat -T -c  
```  
  
 To verify that the command succeeded, display the contents of the table in Query Editor, and enter:  
  
```  
USE AdventureWorks2012;  
GO  
SELECT * FROM Sales.Currency2  
```  
  
### D. Copying a specific column into a data file  
 To copy a specific column, you can use the **queryout** option. The following example copies only the `Name` column of the `Sales.Currency` table into a data file. The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 At the Windows command prompt, enter:  
  
```  
bcp "SELECT Name FROM AdventureWorks.Sales.Currency" queryout Currency.Name.dat -T -c  
```  
  
### E. Copying a specific row into a data file  
 To copy a specific row, you can use the **queryout** option. The following example copies only the row for the contact named `Jarrod Rana` from the `AdventureWorks2012.Person.Person` table into a data file (`Jarrod Rana.dat`).The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 At the Windows command prompt, enter:  
  
```  
bcp "SELECT * FROM AdventureWorks2012.Person.Person WHERE FirstName='Jarrod' AND LastName='Rana' "  queryout "Jarrod Rana.dat" -T -c  
```  
  
### F. Copying data from a query to a data file  
 To copy the result set from a [!INCLUDE[tsql](../includes/tsql-md.md)] statement to a data file, use the **queryout** option. The following example copies the names from the `AdventureWorks2012.Person.Person` table, ordered by last name then first name, into the `Contacts.txt` data file. The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 At the Windows command prompt, enter:  
  
```  
bcp "SELECT FirstName, LastName FROM AdventureWorks2012.Person.Person ORDER BY LastName, Firstname" queryout Contacts.txt -c -T  
```  
  
### G. Creating a non-XML format file  
 The following example creates a non-XML format file, `Currency.fmt`, for the `Sales.Currency` table in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database. The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 At the Windows command prompt, enter:  
  
```  
bcp AdventureWorks2012.Sales.Currency format nul -T -c  -f Currency.fmt  
```  
  
 For more information, see [Non-XML Format Files &#40;SQL Server&#41;](../relational-databases/import-export/xml-format-files-sql-server.md).  
  
### H. Creating an XML format file  
 The following example creates an XML format file named `Currency.xml` for the `Sales.Currency` table in the [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database. The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 At the Windows command prompt, enter:  
  
```  
bcp AdventureWorks2012.Sales.Currency format nul -T -c -x -f Currency.xml  
```  
  
> [!NOTE]  
>  To use the **-x** switch, you must be using a **bcp** 9.0 client. For information about how to use the **bcp** 9.0 client, see "Remarks."  
  
 For more information, see [XML Format Files &#40;SQL Server&#41;](../relational-databases/import-export/xml-format-files-sql-server.md).  
  
### I. Using a format file to bulk import with bcp  
 To use a previously created format file when importing data into an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], use the **-f** switch with the **in** option. For example, the following command bulk copies the contents of a data file, `Currency.dat`, into a copy of the `Sales.Currency` table (`Sales.Currency2`) by using the previously created format file (`Currency.xml`). The example assumes that you are using Windows Authentication and have a trusted connection to the server instance on which you are running the **bcp** command.  
  
 At the Windows command prompt, enter:  
  
```  
bcp AdventureWorks2012.Sales.Currency2 in Currency.dat -T -f Currency.xml  
```  
  
> [!NOTE]  
>  Format files are useful when the data file fields are different from the table columns; for example, in their number, ordering, or data types. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).  
  
## Additional Examples  
 The following topics contain examples of using **bcp**:  
  
-   [Create a Format File &#40;SQL Server&#41;](../relational-databases/import-export/create-a-format-file-sql-server.md)  
  
-   [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)  
  
-   [Keep Identity Values When Bulk Importing Data &#40;SQL Server&#41;](../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)  
  
-   [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)  
  
-   [Specify Field and Row Terminators &#40;SQL Server&#41;](../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)  
  
-   [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
## See Also  
 [Prepare Data for Bulk Export or Import &#40;SQL Server&#41;](../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-quoted-identifier-transact-sql)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)   
 [sp_tableoption &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-tableoption-transact-sql)   
 [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)  
  
  
