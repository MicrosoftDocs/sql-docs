---
title: "Run Python using T-SQL | Microsoft Docs"
ms.custom: ""
ms.date: "04/214/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "Python"
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Run Python Using T-SQL

This example shows how you can run a simple Python script in SQL Server, by using the stored procedure sp_execute_external_script.

## Step 1. Create the test data table

Run the following T-SQL statement to create a mapping table for days of the week.

```SQL
CREATE TABLE PythonTest (
    [DayOfWeek] varchar(10) NOT NULL,
    [Amount] float NOT NULL
    )
GO

INSERT INTO PythonTest VALUES
('Sunday', 10.0),
('Monday', 11.1),
('Tuesday', 12.2),
('Wednesday', 13.3),
('Thursday', 14.4),
('Friday', 15.5),
('Saturday', 16.6),
('Friday', 17.7),
('Monday', 18.8),
('Sunday', 19.9)
GO
```

## Step 2. Run the Hello World script

The following code loads the Python executable, passes the input data, and for each row of input data, updates the day name in the table with a number representing the day-of-week index.

The section containing the message "Hello World" prints two times because the value of @RowsPerRead is set to 5; therefore, the rows in the table must be processed in two calls to Python.

Note that the Python Data Analysis Library, or **pandas**, is required for passing data to SQL Server, and is included by default with Machine Learning Services.

```sql
DECLARE @ParamINT INT = 1234567
DECLARE @ParamCharN VARCHAR(6) = 'INPUT '

print '------------------------------------------------------------------------'
print 'Output parameters (before):'
print FORMATMESSAGE('ParamINT=%d', @ParamINT)
print FORMATMESSAGE('ParamCharN=%s', @ParamCharN)

print 'Dataset (before):'
SELECT * FROM PythonTest

print '------------------------------------------------------------------------'
print 'Dataset (after):'
DECLARE @RowsPerRead INT = 5

execute sp_execute_external_script 
@language = N'Python',
@script = N'
import sys
import os
print("*******************************")
print(sys.version)
print("Hello World")
print(os.getcwd())
print("*******************************")
if ParamINT == 1234567:
       ParamINT = 1
else:
       ParamINT += 1

ParamCharN="OUTPUT"
OutputDataSet = InputDataSet

global daysMap

daysMap = {
       "Monday" : 1,
       "Tuesday" : 2,
       "Wednesday" : 3,
       "Thursday" : 4,
       "Friday" : 5,
       "Saturday" : 6,
       "Sunday" : 7
       }

OutputDataSet["DayOfWeek"] = pandas.Series([daysMap[i] for i in OutputDataSet["DayOfWeek"]], index = OutputDataSet.index, dtype = "int32")
', 
@input_data_1 = N'SELECT * FROM PythonTest', 
@params = N'@r_rowsPerRead INT, @ParamINT INT OUTPUT, @ParamCharN CHAR(6) OUTPUT',
@r_rowsPerRead = @RowsPerRead,
@paramINT = @ParamINT OUTPUT,
@paramCharN = @ParamCharN OUTPUT
with result sets (("DayOfWeek" int null, "Amount" float null))

print 'Output parameters (after):'
print FORMATMESSAGE('ParamINT=%d', @ParamINT)
print FORMATMESSAGE('ParamCharN=%s', @ParamCharN)
GO
```
### Expected Results

The stored procedure returns the original data, applies the script, and then returns the modified data. 

|DayOfWeek (before)| Amount|DayOfWeek (after) |
|-----|-----|-----|
|Sunday|10|7|
|Monday|11.1|1|
|Tuesday|12.2|2|
|Wednesday|13.3|3|
|Thursday|14.4|4|
|Friday|15.5|5|
|Saturday|16.6|6|
|Friday|17.7|5|
|Monday|18.8||
|Sunday|19.9|7|

### Messages

Status messages or errors returned to the Python console are returned as messages in the Query window.

------------------------------------------------------------------------
Output parameters (before):
ParamINT=1234567
ParamCharN=INPUT 
Dataset (before):

(10 row(s) affected)
------------------------------------------------------------------------
Dataset (after):
STDOUT message(s) from external script: 
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\revoscalepy
*******************************
3.5.2 |Anaconda 4.2.0 (64-bit)| (default, Jul  5 2016, 11:41:13) [MSC v.1900 64 bit (AMD64)]
Hello World
C:\PROGRA~1\MICROS~2\MSSQL1~1.MSS\MSSQL\EXTENS~1\MSSQLSERVER01\7A70B3FB-FBA2-4C52-96D6-8634DB843229
*******************************
*******************************
3.5.2 |Anaconda 4.2.0 (64-bit)| (default, Jul  5 2016, 11:41:13) [MSC v.1900 64 bit (AMD64)]
Hello World
C:\PROGRA~1\MICROS~2\MSSQL1~1.MSS\MSSQL\EXTENS~1\MSSQLSERVER01\7A70B3FB-FBA2-4C52-96D6-8634DB843229
*******************************


(10 row(s) affected)
Output parameters (after):
ParamINT=2
ParamCharN=OUTPUT
