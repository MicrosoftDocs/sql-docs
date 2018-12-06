---
title: dwloader Command-Line Loader - Parallel Data Warehouse | Microsoft Docs
description: dwloader is a Parallel Data Warehouse (PDW) command-line tool that loads table rows in bulk into an existing table.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# dwloader Command-Line Loader for Parallel Data Warehouse
**dwloader** is a Parallel Data Warehouse (PDW) command-line tool that loads table rows in bulk into an existing table. When loading rows, you can add all rows to the end of the table (*append mode* or *fastappend mode*), append new rows and update existing rows (*upsert mode*), or delete all existing rows before loading and then insert all rows into an empty table (*reload mode*).  
  
**Process for loading data**  
  
1.  Prepare the source data.  
  
    Use your own ETL process to create the source data you wish to load. The source data must be formatted to match the schema of the destination table. Store the source data into one or more text files and copy the text files into the same directory on your Loading server. For information about the Loading server, see [Acquire and Configure a Loading Server](acquire-and-configure-loading-server.md)  
  
2.  Prepare the loading options.  
  
    Decide which loading options you will use. Store the load options in a configuration file. Copy the configuration file to a local location on your Loading server. The **dwloader** configuration options are described in this topic.  
  
3.  Prepare the load failure options.  
  
    Decide how you want **dwloader** to handle rows that fail to load. To perform the load, **dwloader** first loads the data into a staging table and then transfers the data to the destination table. As the Loader loads data into the staging table, it tracks the number of rows that fail to load. For example, rows that are not formatted correctly will fail to load. Failed rows are copied to a rejection file. By default, the load aborts after the first rejection unless you specify a different rejection threshold.  
  
4.  Install **dwloader**.  
  
    Install dwloader onto the Loading server if it is not already installed. 

<!-- MISSING LINK
	See [Install dwloader Command-Line Loader &#40;SQL Server PDW&#41;](install-dwloader-command-line-loader-sql-server-pdw.md). 
--> 
  
5.  Run **dwloader**.  
  
    Log in to the Loading server and run the executable **dwloader.exe** with the appropriate command-line options.  
  
6.  Verify the results.  
  
    You can check the failed rows file (specified with -R) to see if any rows failed to load. If this file is empty, all rows loaded successfully. **dwloader** is transactional, so if any step fails (other than rejected rows), all steps will roll back to their initial state.  
  
<!-- 
![Topic link icon](media/topic-link.gif "Topic_Link")[Syntax Conventions](syntax-conventions-sql-server-pdw.md)  
-->  


## Syntax  
  
```  
dwloader.exe { -h }  
  
dwloader.exe   
    {  
        { -U login_name  -P password  }  
        | -W  
    }  
    [ -f parameter_file ]  
    [ -S target_appliance ]  
    { -T target_database_name . [ schema ] . table_name }   
    { -i source_data_location } [ <source_data_options> ]  
    { -R load_failure_file_name } [ <load_failure_options> ]  
    [ <loading_options> ]  
}  
  
<source_data_options> ::=  
{  
    [ -fh number_header_rows ]  
    [ < variable_length_column_options > | < fixed_width_column_options > ]  
    [ -D { mdy | myd | ymd | ydm | dmy | dym | custom_date_format } ]  
    [ -dt datetime_format_file ]  
}  
  
<variable_length_column_options> ::=  
{  
    [ -e character_encoding ]  
    -r row_delimiter   
    [ -s string_delimiter ]  
    -t field_delimiter   
}  
  
<fixed_width_column_options> ::=  
{  
    -w fixed_width_config_file   
    [ -e character_encoding ]   
    -r row_delimiter   
}  
  
<load_failure_options> ::=  
{  
    [ -rt { value | percentage } ]  
    [ -rv reject_value ]  
    [ -rs reject_sample_value ]  
}  
  
<loading_options> ::=  
{  
    [ -d staging_database_name ]  
    [ -M { append | fastappend | upsert -K merge_column [ ,...n ] | reload } ]  
    [ -b batchsize ]   
    [ -c ]  
    [ -E ]  
    [ -m ]  
    [ -N ]  
    [ -se ]   
}  
```  
  
## Arguments  
**-h**  
Displays simple Help information about using the Loader. Help only displays if no other command-line parameters are specified.  
  
**-U** *login_name*  
A valid SQL Server Authentication login with appropriate permissions to perform the load.  
  
**-P** *password*  
The password for a SQL Server Authentication *login_name*.  
  
**-W**  
Use Windows Authentication. (No *login_name* or *password* required.) 

<!-- MISSING LINK
For information about configuring Windows Authentication, see [Security - Configure Domain Trusts](security-configure-domain-trusts.md).  
-->
  
**-f** *parameter_file_name*  
Use a parameter file, *parameter_file_name*, in place of command-line parameters. *parameter_file_name* can contain any command-line parameter except *user_name* and *password*. If a parameter is specified on the command line and in the parameter file, the command line overrides the file parameter.  
  
The parameter file contains one parameter, without the **-** prefix, per line.  
  
Examples:  
  
`rt=percentage`  
  
`rv=25`  
  
**-S***target_appliance*  
Specifies the SQL Server PDW appliance that will receive the loaded data.  
  
*For Infiniband connections*, *target_appliance* is specified as <appliance-name>-SQLCTL01. To configure this named connection, see [Configure InfiniBand Network Adapters](configure-infiniband-network-adapters.md).  
  
For Ethernet connections, *target_appliance* is the IP address for the Control node cluster.  
  
If omitted, dwloader defaults to the value that was specified when dwloader was installed. 

<!-- MISSING LINK
For more information about this install option, see [Install dwloader Command-Line Loader](install-dwloader.md).  
-->
  
**-T** *target_database_name.*[*schema*].*table_name*  
The three-part name for the destination table.  
  
**-I***source_data_location*  
The location of one or more source files to load. Each source file must be a text file or a text file that is compressed with gzip. Only one source file can be compressed into each gzip file.  
  
To format a source file:  
  
-   The source file must be formatted in accordance with the load options.  
  
-   Each line in a source file contains the data for one table row. The source data must match the schema of the destination table. Column order and data types must also match. Each field in the row represents a column in the destination table.  
  
-   By default, the fields are variable length and separated by a delimiter. To specify the delimiter type, use the <variable_length_column_options> command-line options. To specify fixed length fields, use the <fixed_width_column_options> command-line options.  
  
To specify the source data location:  
  
-   The source data location can be a network path or a local path to a directory on the loading server.  
  
-   To specify all the files in a directory, enter the directory path followed by the * wildcard character.  The loader does not load files from any subdirectories that are in the source data location.. The loader errors when a directory exists in a gzip file.  
  
-   To specify some of the files in a directory, use a combination of characters and the * wildcard.  
  
To load multiple files with one command:  
  
-   All files must exist in the same directory.  
  
-   The files must be all text files, all gzip files, or a combination of both text and gzip files.  
  
-   None of the files can contain header information.  
  
-   All files must use the same character encoding type. See the -e option.  
  
-   All files must be loaded into the same table.  
  
-   All files will be concatenated and loaded as though they are one file, and the rejected rows will go to a single reject file.  
  
Examples:  
  
-   -i \\\loadserver\loads\daily\\*.gz  
  
-   -i \\\loadserver\loads\daily\\*.txt  
  
-   -i \\\loadserver\loads\daily\monday.*  
  
-   -i \\\loadserver\loads\daily\monday.txt  
  
-   -i \\\loadserver\loads\daily\\*  
  
**-R** *load_failure_file_name*  
If there are load failures, **dwloader** stores the row that failed to load and the failure description the failure information in a file named *load_failure_file_name*. If this file already exists, dwloader will overwrite the existing file. *load_failure_file_name* is created when the first failure occurs. If all rows load successfully, *load_failure_file_name* is not created.  
  
**-fh** *number_header_rows*  
The number of lines (rows) to ignore at the beginning of *source_data_file_name*. The default is 0.  
  
<variable_length_column_options>  
The options for a *source_data_file_name* that has character-delimited variable-length columns. By default, *source_data_file_name* contains ASCII characters in variable-length columns.  
  
For ASCII files, NULLs are represented by placing delimiters consecutively. For example, in a pipe-delimited file ("|"), a NULL is indicated by "||". In a comma-delimited file, a NULL is indicated by ",,". Additionally, the **-E** (--emptyStringAsNull) option must be specified. For more information on -E, see below.  
  
**-e** *character_encoding*  
Specifies a character-encoding type for the data to be loaded from the data file. Options are ASCII (default), UTF8, UTF16, or UTF16BE, where UTF16 is little endian and UTF16BE is big endian. These options are case insensitive.  
  
**-t** *field_delimiter*  
The delimiter for each field (column) in the row. The field delimiter is one or more of these ASCII escape characters or ASCII hex values..  
  
|Name|Escape Character|Hex Character|  
|--------|--------------------|-----------------|  
|Tab|\t|0x09|  
|Carriage return (CR)|\r|0x0d|  
|Line feed (LF)|\n|0x0a|  
|CRLF|\r\n|0x0d0x0a|  
|Comma|','|0x2c|  
|Double quote|\\"|0x22|  
|Single quote|\\'|0x27|  
  
To specify the pipe character on the command line, enclose it with double quotes, "|". This will avoid misinterpretation by the command-line parser. Other characters are enclosed with single quotes.  
  
Examples:  
  
-t "|"  
  
-t ' '  
  
-t 0x0a  
  
-t \t  
  
-t '~|~'  
  
**-r** *row_delimiter*  
The delimiter for each row of the source data file. The row delimiter is one or more ASCII values.  
  
To specify a carriage return (CR), linefeed (LF), or tab character as a delimiter, you can use the escape characters (\r, \n, \t) or their hex values (0x, 0d, 09). To specify any other special characters as delimiters, use their hex value.  
  
Examples of CR+LF:  
  
-r \r\n  
  
-r 0x0d0x0a  
  
Examples of CR:  
  
-r \r  
  
-r 0x0d  
  
Examples of LF:  
  
-r \n  
  
-r 0x0a  
  
An LF is required for Unix. A CR is required for Windows.  
  
**-s** *string_delimiter*  
The delimiter for string data type field of a text-delimited input file. The string delimiter is one or more ASCII values.  It can be specified as a character (e.g., -s * ) or as a hex value (e.g., -s 0x22 for a double quote).  
  
Examples:  
  
-s *  
  
-s 0x22  
  
< fixed_width_column_options>  
The options for a source data file that has fixed-length columns. By default, *source_data_file_name* contains ASCII characters in variable-length columns.  
  
Fixed width columns are not supported when -e is UTF8.  
  
**-w** *fixed_width_config_file*  
Path and name of the configuration file that specifies the number of characters in each column. Every field must be specified.  
  
This file must reside on the Loading server. The path can be a UNC, relative, or absolute path. Each line in *fixed_width_config_file* contains the name of one column and the number of characters for that column. There is one line per column, as follows, and the order in the file must match the order in the destination table:  
  
*column_name*=*num_chars*  
  
*column_name*=*num_chars*  
  
Example fixed width config file:  
  
SalesCode=3  
  
SalesID=10  
  
Example lines in *source_data_file_name*:  
  
230Shirts0056  
  
320Towels1356  
  
In the previous example, the first loaded row will have SalesCode='230' and SalesID='Shirts0056'. The second loaded row will have SalesCode='320' and SaleID='Towels1356'.  
  
For information on how to handle leading and trailing spaces or data type conversion in fixed width mode, see [Data type conversion rules for dwloader](dwloader-data-type-conversion-rules.md).  
  
**-e** *character_encoding*  
Specifies a character-encoding type for the data to be loaded from the data file. Options are ASCII (default), UTF8, UTF16, or UTF16BE, where UTF16 is little endian and UTF16BE is big endian. These options are case insensitive.  
  
Fixed width columns are not supported when -e is UTF8.  
  
**-r** *row_delimiter*  
The delimiter for each row of the source data file. The row delimiter is one or more ASCII values.  
  
To specify a carriage return (CR), linefeed (LF), or tab character as a delimiter, you can use the escape characters (\r, \n, \t) or their hex values (0x, 0d, 09). To specify any other special characters as delimiters, use their hex value.  
  
Examples of CR+LF:  
  
-r \r\n  
  
-r 0x0d0x0a  
  
Examples of CR:  
  
-r \r  
  
-r 0x0d  
  
Examples of LF:  
  
-r \n  
  
-r 0x0a  
  
An LF is required for Unix. A CR is required for Windows.  
  
**-D** { **ymd** | ydm | mdy | myd |  dmy | dym | *custom_date_format* }  
Specifies the order of month (m), day (d), and year (y) for all datetime fields in the input file. The default order is ymd. To specify multiple order formats for the same source file, use the -dt option.  
  
ymd | dmy  
ydm and dmy allow the same input formats. Both allow the year to be at the beginning or the end of the date. For example, for both **ydm** and **dmy** date formats, you could have 2013-02-03 or 02-03-2013 in the input file.  
  
ydm  
You can only load input formatted as ydm into columns of data type datetime and smalldatetime. You cannot load ydm values into a column of the datetime2, date, or datetimeoffset data type.  
  
mdy  
mdy allows <month><space><day><comma><year>.  
  
Examples of mdy input data for January 1, 1975:  
  
-   January 1, 1975  
  
-   Jan 01, 75  
  
-   Jan/1/75  
  
-   01011975  
  
myd  
Input file examples for March 04,2010: 03-2010-04, 3/2010/4  
  
dym  
Input file examples for March 04, 2010: 04-2010-03, 4/2010/3  
  
*custom_date_format*  
*custom_date_format* is a custom date format (e.g., MM/dd/yyyy ) and included for backward compatibility only. dwloader does not enfoce the custom date format. Instead, when you specify a custom date format, **dwloader** will convert it to the corresponding setting of ymd, ydm,  mdy,  myd,  dym, or dmy.  
  
For example, if you specify -D MM/dd/yyyy, dwloader expects all date input to be ordered with month first, then day, and then year (mdy). It does not enforce 2 character months, 2 digit days, and 4 digit years as specified by the custom date format. Here are some examples of ways dates can be formatted in the input file when the date format is -D MM/dd/yyyy: 01/02/2013, Jan.02.2013, 1/2/2013  
  
For more comprehensive formatting information, see [Data type conversion rules for dwloader](dwloader-data-type-conversion-rules.md).  
  
**-dt** *datetime_format_file*  
Each datetime format is specified in a file named *datetime_format_file*. Unlike the command-line parameters, file parameters that include spaces must not be enclosed in double quotes. You cannot alter the datetime format as you load data. The source data file and its corresponding column in the destination table must have the same format.  
  
Each line contains the name of a column in the destination table and its datetime format.  
  
Examples:  
  
`LastReceiptDate=ymd`  
  
`ModifiedDate=dym`  
  
**-d** *staging_database_name*  
The database name that will contain the staging table. The default is the database specified with the -T option, which is the database for the destination table. For more information about using a staging database, see [Create the Staging Database](staging-database.md).  
  
**-M** *load_mode_option*  
Specifies whether to append, upsert, or reload data. The default mode is append.  
  
append  
The Loader inserts rows at the end of existing rows in the destination table.  
  
fastappend  
The Loader inserts rows directly, without using a temporary table, to the end of existing rows in the destination table. fastappend requires the multi-transaction (-m) option. A staging database cannot be specified when using fastappend. There is no rollback with fastappend, which means that recovery from a failed or aborted load must be handled by your own load process.  
  
upsert **-K**  *merge_column* [ ,...*n* ]  
The Loader uses the SQL Server Merge statement to update existing rows and insert new rows.  
  
The -K option specifies the column or columns to base the merge on. These columns form a merge key, which should represent a unique row. If the merge key exists in the destination table, the row is updated. If the merge key doesn't exist in the destination table, the row is appended.  
  
For hash-distributed tables, the merge key must be or include the distribution column.  
  
For replicated tables, the merge key is the combination of one or more columns. These columns are specified according to the needs of the application.  
  
Multiple columns must be comma-separated with no spaces, or comma-separated with spaces and enclosed in single quotes.  
  
If two rows in the source table have matching merge key values, their respective rows must be identical.  
  
reload  
The Loader truncates the destination table before it inserts the source data.  
  
**-b** *batchsize*  
Recommended only for use by Microsoft Support, *batchsize* is the SQL Server batch size for the bulk copy that DMS performs into SQL Server instances on the Compute nodes.  When *batchsize* is specified, SQL Server PDW will override the batch load size that is calculated dynamically for each load.  
  
Beginning with SQL Server 2012 PDW, the Control node dynamically computes a batch size for each load by default. This automatic calculation is based on several parameters such as memory size, target table type, target table schema, load type, file size, and the user's resource class.  
  
For example, if the load mode is FASTAPPEND and the table has a clustered columnstore index, SQL Server PDW will by default attempt to use a batch size of 1,048,576 so that rowgroups will become CLOSED and load directly into the columnstore without going through the delta store. If memory does not allow the batch size of 1,048,576, dwloader will choose a smaller batchsize.  
  
If the load type is FASTAPPEND, the *batchsize* applies to loading data into the table, otherwise *batchsize* applies to loading data into the staging table.  
  
<reject_options>  
Specifies options for determining the number of load failures that the Loader will allow. If the load failures exceed the threshold, the loader will halt and not commit any rows.  
  
**-rt** { **value** | percentage }  
Specifies whether the -*reject_value* in the **-rv** *reject_value* option is a literal number of rows (value) or a rate of failure (percentage). The default is value.  
  
The percentage option is a real-time calculation that occurs at intervals according to the -rs option.  
  
For example, if the Loader attempts to load 100 rows and 25 fail and 75 succeed, then the failure rate is 25%.  
  
**-rv** *reject_value*  
Specifies the number or percentage of row rejections to allow before halting the load. The **-rt** option determines if *reject_value* refers to the number of rows or the percentage of rows.  
  
The default *reject_value* is 0.  
  
When used with -rt value, the loader stops the load when the rejected row count exceeds reject_value.  
  
When use with -rt percentage, the loader computes the percentage at intervals (-rs option). Therefore, the percentage of failed rows can exceed *reject_value*.  
  
**-rs** *reject_sample_size*  
Used with the `-rt percentage` option to specify the incremental percentage checks. For example, if reject_sample_size is 1000, the Loader will calculate the percentage of failed rows after it has attempted to load 1000 rows. It recalculates the percentage of failed rows after it attempts to load each additional 1000 rows.  
  
**-c**  
Removes white space characters from the left and right side of char, nchar, varchar, and nvarchar fields. Converts each field that contains only white space characters to the empty string.  
  
Examples:  
  
'        ' gets truncated to ''  
  
'    abc     ' gets truncated to 'abc'  
  
When -c is used with -E, the -E operation occurs first. Fields that contain only white space characters are converted to the empty string, and not to NULL.  
  
**-E**  
Convert empty strings to NULL. The default is to not perform these conversions.  
  
**-m**  
Use multi-transaction mode for the second phase of loading; when loading data from the staging table into a distributed table.  
  
With **-m**, SQL Server PDW performs and commits loads in parallel. This performs much faster than the default loading mode, but is not transaction-safe.  
  
Without **-m**, SQL Server PDW performs and commits loads serially across the distributions within each Compute node, and concurrently across the Compute nodes. This method is slower than multi-transaction mode, but is transaction-safe.  
  
**-m** is optional for *append*, *reload*, and *upsert*.  
  
**-m** is required for fastappend.  
  
**-m** cannot be used with replicated tables.  
  
**-m** applies only to the second loading phase. It does not apply to the first loading phase; loading data into the staging table.  
  
There is no rollback with multi-transaction mode, which means that recovery from a failed or aborted load must be handled by your own load process.  
  
We recommend using **-m** only when loading into an empty table, so that you can recover without data loss. To recover from a load failure: drop the destination table, resolve the load issue, re-create the destination table, and run the load again.  
  
**-N**  
Verify the target appliance has a valid SQL Server PDW certificate from a trusted authority. Use this to help ensure your data is not being hijacked by an attacker and sent to an unauthorized location. The certificate must already be installed on the appliance. The only supported way to install the certificate is for the appliance administrator to install it by using the Configuration Manager tool. Ask your appliance administrator if you are not sure whether the appliance has a trusted certificate installed.  
  
**-se**  
Skip loading empty files. This also skips uncompressing empty gzip files.  
  
## Return Code Values  
0 (success) or other integer value (failure)  
  
In a command window or batch file, use `errorlevel` to display the return code. For example:  
  
```  
dwloader  
echo ReturnCode=%errorlevel%  
if not %errorlevel%==0 echo Fail  
if %errorlevel%==0 echo Success  
```  
  
When using PowerShell, use `$LastExitCode`.  
  
## Permissions  
Requires LOAD permission and applicable permissions (INSERT, UPDATE, DELETE) on the destination table. Requires CREATE permission (for creating a temporary table) on the staging database. If a staging database is not used, then CREATE permission is required on the destination database. 

<!-- MISSING LINK
For more information, see [Grant permissions to load data](grant-permissions-to-load-data.md).  
-->
  
## General Remarks  
For information on data type conversions when loading with dwloader, see [Data type conversion rules for dwloader](dwloader-data-type-conversion-rules.md).  
  
If a parameter includes one or more spaces, enclose the parameter with double quotes.  
  
You should run the Loader from its installed location. The dwloader executable is pre-installed with the appliance and is located in the C:\Program Files\Microsoft SQL Server Data Warehouse\DWLoader directory.  
  
You can override a parameter that is specified in the parameter file (-f option) by specifying it as a command-line parameter.  
  
You can run multiple instances of the Loader simultaneously. The maximum number of Loader instances is pre-configured and cannot be changed. 

<!-- MISSING LINK
For the maximum number of loads per appliance, see [Minimum and Maximum Values](minimum-maximum-values.md)  
-->
  
Loaded data might require more or less space on the appliance than in the source location. You can perform test imports with subsets of data to estimate disk consumption.  
  
Although **dwloader** is a transaction process and will roll back gracefully on failure, it cannot be rolled back once the bulk load has been completed successfully. To cancel an active **dwloader** process, type CTRL+C.  
  
## Limitations and Restrictions  
The total size of all loads occurring concurrently must be smaller than LOG_SIZE for the database, and we recommend the total size of all concurrent loads is less than 50% of the LOG_SIZE. To achieve this size limitation, you can split large loads  into multiple batches. For more information on LOG_SIZE, see [CREATE DATABASE](../t-sql/statements/create-database-parallel-data-warehouse.md)  
  
When loading multiple files with one load command, all rejected rows are written to the same reject file. The reject file does not show which input file contains each rejected row.  
  
The empty string should not be used as a delimiter . When an empty string is used as a row delimiter, the load will fail. When used as column delimiter, the load ignores the delimiter and continues to use the default "|" as the column delimiter. When used as string delimiter, the empty string is ignored and the default behavior is applied.  
  
## Locking Behavior  
**dwloader** locking behavior varies depending on the *load_mode_option*.  
  
-   **append** - Append is the recommended and the most common option. Append loads data into a staging table. The locking is described in detail below.  
  
-   **fast append** - Fast-append loads directly into the final table taking an ExclusiveUpdate table lock, and is the only mode that does not use a staging table.  
  
-   **reload** - Reload loads data into a staging table and requires an exclusive lock on both the staging table and the final table. Reload is not recommended for concurrent operations.  
  
-   **upsert** - Upsert loads data into a staging table, and then performs a merge operation from the staging table to the final table. Upsert does not require exclusive locks on the final table. Performance may vary when using upsert. Test the behavior in your environment.  
  
### Locking Behavior  
**A append mode locking**  
  
Append can be run in multi-transactional mode (using the -m argument) but it is not transaction safe. Therefore append should be used as a transactional operation (without using the -m argument). Unfortunately, during the final INSERT-SELECT operation, transactional mode is currently about six times slower than the multi-transactional mode.  
  
The append mode loads data in two phases. Phase one loads data from the source file into a staging table concurrently (fragmentation can occur). Phase two loads data from the staging table to the final table. The second phase performs an **INSERT INTO...SELECT WITH (TABLOCK)** operation. The following table shows the locking behavior on the final table, and logging behavior when using append mode:  
  
|Table Type|Multi-transaction<br />Mode (-m)|Table is Empty|Concurrency Supported|Logging|  
|--------------|-----------------------------------|------------------|-------------------------|-----------|  
|Heap|Yes|Yes|Yes|Minimal|  
|Heap|Yes|No|Yes|Minimal|  
|Heap|No|Yes|No|Minimal|  
|Heap|No|No|No|Minimal|  
|Cl|Yes|Yes|No|Minimal|  
|Cl|Yes|No|Yes|Full|  
|Cl|No|Yes|No|Minimal|  
|Cl|No|No|Yes|Full|  
  
The above table shows **dwloader** using the append mode loading into a heap or a clustered index (CI) table, with or without the multi-transactional flag, and loading into an empty table or a non-empty table. The locking and logging behavior of each such combination of load is displayed in the table. For instance, loading (2nd) phase with the append mode into a clustered index without multi-transactional mode and into an empty table will have PDW create an exclusive lock on the table and logging is minimal. This means that a customer will not be able to load (2nd) phase and query concurrently into an empty table. However, when loading with the same configuration into a non-empty table, PDW will not issue an exclusive lock on the table and concurrency is possible. Unfortunately, full logging occurs, slowing the process.  
  
## Examples  
  
### A. Simple dwloader example  
The following example shows initiation of the **Loader** with only the required options selected. Other options are taken from the global configuration file, *loadparamfile.txt*.  
  
Example using SQL Server Authentication.  
  
```  
--Load over Ethernet  
dwloader.exe -S 10.192.63.148 -U mylogin -P 123jkl -f /configfiles/loadparamfile.txt  
  
--Load over InfiniBand to appliance named MyPDW  
dwloader.exe -S MyPDW-SQLCTL01 -U mylogin -P 123jkl -f /configfiles/loadparamfile.txt  
```  
  
The same example using Windows Authentication.  
  
```  
--Load over Ethernet  
dwloader.exe -S 10.192.63.148 -W -f /configfiles/loadparamfile.txt  
  
--Load over InfiniBand to appliance named MyPDW  
dwloader.exe -S MyPDW-SQLCTL01 -W -f /configfiles/loadparamfile.txt  
```  
  
Example using arguments for a source file and error file.  
  
```  
--Load over Ethernet  
dwloader.exe -U mylogin -P 123jkl -S 10.192.63.148  -i C:\SQLData\AWDimEmployees.csv -T AdventureWorksPDW2012.dbo.DimEmployees -R C:\SQLData\LoadErrors  
```  
  
### B. Load Data into an AdventureWorks Table  
The following example is part of a batch script that loads data into **AdventureWorksPDW2012**.  To view the full script, open the aw_create.bat file that ships with the **AdventureWorksPDW2012** installation package. 

<!-- Missing link
For more information, see [Install AdventureWorksPDW2012](install-adventureworkspdw2012.md).  
-->

The following script snippet uses dwloader to load data into the DimAccount and DimCurrency tables. This script is using an Ethernet address. If it was using InfiniBand, server would be *<appliance_name>*`-SQLCTL01`.  
  
```  
set server=10.193.63.134  
set user=<MyUser>  
set password=<MyPassword>  
  
set schema=AdventureWorksPDW2012.dbo  
set load="C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\dwloader.exe"  
set mode=reload  
  
--Loads data into the AdventureWorksPDW2012.dbo.DimAccount table  
--Source data is stored in the file DimAccount.txt,   
--which is in the current directory.  
  
set t1=DimAccount  
%load% -S %server% -E -M %mode% -e Utf16 -i .\%t1%.txt -T %schema%.%t1% -R %t1%.bad -t "|" -r \r\n -U %user% -P %password%   
  
--Loads data from the DimCurrency.txt file into  
--AdventureWorksPDW2012.dbo.DimCurrency  
set t1=DimCurrency  
%load% -S %server% -E -M %mode% -e Utf16 -i .\%t1%.txt -T %schema%.%t1% -R %t1%.bad -t "|" -r \r\n -U %user% -P %password%  
```  
  
The following is the DDL for the DimAccount Table.  
  
```  
CREATE TABLE DimAccount(  
AccountKey int NOT NULL,  
ParentAccountKey int,  
AccountCodeAlternateKey int,  
ParentAccountCodeAlternateKey int,  
AccountDescription nvarchar(50),  
AccountType nvarchar(50),  
Operator nvarchar(50),  
CustomMembers nvarchar(300),  
ValueType nvarchar(50),  
CustomMemberOptions nvarchar(200))  
with (CLUSTERED INDEX(AccountKey),  
DISTRIBUTION = REPLICATE);  
```  
  
The following is an example of the data file, DimAccount.txt, that contains data to load into the table DimAccount.  
  
```  
--Sample of data in the DimAccount.txt load file.  
  
1||1||Balance Sheet||~||Currency|  
2|1|10|1|Assets|Assets|+||Currency|  
3|2|110|10|Current Assets|Assets|+||Currency|  
4|3|1110|110|Cash|Assets|+||Currency|  
5|3|1120|110|Receivables|Assets|+||Currency|  
6|5|1130|1120|Trade Receivables|Assets|+||Currency|  
7|5|1140|1120|Other Receivables|Assets|+||Currency|  
8|3|1150|110|Allowance for Bad Debt|Assets|+||Currency|  
9|3|1160|110|Inventory|Assets|+||Currency|  
10|9|1162|1160|Raw Materials|Assets|+||Currency|  
11|9|1164|1160|Work in Process|Assets|+||Currency|  
12|9|1166|1160|Finished Goods|Assets|+||Currency|  
13|3|1170|110|Deferred Taxes|Assets|+||Currency|  
```  
  
### C. Load Data from the Command Line  
The script in Example B can be replaced by entering all the parameters on the command line, as shown in the following example.  
  
```  
C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\dwloader.exe -S <Control node IP> -E -M reload -e UTF16 -i .\DimAccount.txt -T AdventureWorksPDW2012.dbo.DimAccount -R DimAccount.bad -t "|" -r \r\n -U <login> -P <password>  
```  
  
Description of the command-line parameters:  
  
-   *C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\dwloader.exe* is the installed location of dwloader.exe.  
  
-   *-S* is followed by the IP address of the Control node.  
  
-   *-E* specifies to load empty strings as NULL.  
  
-   *-M reload* specifies to truncate the destination table before it inserts the source data.  
  
-   *-e UTF16* indicates the source file uses the little endian character encoding type.  
  
-   *-i .\DimAccount.txt* specifies the data is in a file called DimAccount.txt which exists in the current directory.  
  
-   *-T AdventureWorksPDW2012.dbo.DimAccount* specifies the 3-part name of the table to receive the data.  
  
-   *-R DimAccount.bad* specifies the rows that fail to load will be written to a file called DimAccount.bad.  
  
-   *-t "|"* indicates the fields in the input file, DimAccount.txt, are separated with the pipe character.  
  
-   *-r \r\n* specifies each row in DimAccount.txt ends with a carriage return and a line feed character.  
  
-   *-U <login_name> -P <password>* specifies the login and password for the login that has permissions to perform the load.  
  

<!-- MISSING LINK

## See Also  
[Common Metadata Query Examples](metadata-query-examples.md)  

-->
  
