---
title: 'Tutorial: Regex string search in Java'
description: This tutorial shows you how to use SQL Server Language Extensions and run Java code that search a string with regular expressions (regex).
author: rothja
ms.author: jroth 
ms.date: 11/05/2019
ms.topic: tutorial
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Tutorial: Search for a string using regular expressions (regex) in Java
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

This tutorial shows you how to use [SQL Server Language Extensions](../language-extensions-overview.md) to create a Java class that receives two columns (ID and text) from SQL Server and a regular expression (regex) as an input parameter. The class returns two columns back to SQL Server (ID and text).

For a given text in the text column sent to the Java class, the code checks if the given regular expression is fulfilled, and returns that text together with the original ID.

This sample code uses a regular expression that checks if a text contains the word "Java" or "java".

## Prerequisites

+ SQL Server 2019 Database Engine instance with the extensibility framework and Java programming extension [on Windows](../install/windows-java.md) or [on Linux](../../linux/sql-server-linux-setup-language-extensions-java.md). For more information, see [Language Extension in SQL Server 2019](../language-extensions-overview.md). For more information about coding requirements, see [How to call Java in SQL Server](../how-to/call-java-from-sql.md).

+ SQL Server Management Studio or Azure Data Studio for executing T-SQL.

+ Java SE Development Kit (JDK) 8 or JRE 8 on Windows or Linux.

+ The **mssql-java-lang-extension.jar** file from the [Microsoft Java Extensibility SDK for Microsoft SQL Server](../how-to/extensibility-sdk-java-sql-server.md) .

Command-line compilation using **javac** is sufficient for this tutorial.

## Create sample data

First, create a new database and populate a **testdata** table with **ID** and **text** columns.

```sql
CREATE DATABASE javatest
GO

USE javatest
GO

CREATE TABLE testdata (
    id int NOT NULL,
    "text" nvarchar(100) NOT NULL
)
GO

-- Insert data into test table
INSERT INTO testdata(id, "text") VALUES (1, 'This sentence contains java')
INSERT INTO testdata(id, "text") VALUES (2, 'This sentence does not')
INSERT INTO testdata(id, "text") VALUES (3, 'I love Java!')
GO
```

## Create the main class

In this step, create a class file called **RegexSample.java** and copy the following Java code into that file.

This main class is importing the SDK, which means that the jar file downloaded in step1 needs to be discoverable from this class.

```java
package pkg;

import com.microsoft.sqlserver.javalangextension.PrimitiveDataset;
import com.microsoft.sqlserver.javalangextension.AbstractSqlServerExtensionExecutor;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.ListIterator;
import java.util.regex.*;

public class RegexSample extends AbstractSqlServerExtensionExecutor {
    private Pattern expr;

    public RegexSample() {
        // Setup the expected extension version, and class to use for input and output dataset
        executorExtensionVersion = SQLSERVER_JAVA_LANG_EXTENSION_V1;
        executorInputDatasetClassName = PrimitiveDataset.class.getName();
        executorOutputDatasetClassName = PrimitiveDataset.class.getName();
    }
    
    public PrimitiveDataset execute(PrimitiveDataset input, LinkedHashMap<String, Object> params) {
        // Validate the input parameters and input column schema
        validateInput(input, params);

        int[] inIds = input.getIntColumn(0);
        String[] inValues = input.getStringColumn(1);
        int rowCount = inValues.length;

        String regexExpr = (String)params.get("regexExpr");
        expr = Pattern.compile(regexExpr);

        System.out.println("regex expression: " + regexExpr);

        // Lists to store the output data
        LinkedList<Integer> outIds = new LinkedList<Integer>();
        LinkedList<String> outValues = new LinkedList<String>();

        // Evaluate each row
        for(int i = 0; i < rowCount; i++) {
            if (check(inValues[i])) {
                outIds.add(inIds[i]);
                outValues.add(inValues[i]);
            }
        }

        int outputRowCount = outValues.size();

        int[] idOutputCol = new int[outputRowCount];
        String[] valueOutputCol = new String[outputRowCount];

        // Convert the list of output columns to arrays
        outValues.toArray(valueOutputCol);

        ListIterator<Integer> it = outIds.listIterator(0);
        int rowId = 0;

        System.out.println("Output data:");
        while (it.hasNext()) {
            idOutputCol[rowId] = it.next().intValue();

            System.out.println("ID: " + idOutputCol[rowId] + " Value: " + valueOutputCol[rowId]);
            rowId++;
        }

        // Construct the output dataset
        PrimitiveDataset output = new PrimitiveDataset();

        output.addColumnMetadata(0, "ID", java.sql.Types.INTEGER, 0, 0);
        output.addColumnMetadata(1, "Text", java.sql.Types.NVARCHAR, 0, 0);

        output.addIntColumn(0, idOutputCol, null);
        output.addStringColumn(1, valueOutputCol);

        return output;
    }

    private void validateInput(PrimitiveDataset input, LinkedHashMap<String, Object> params) {
        // Check for the regex expression input parameter
        if (params.get("regexExpr") == null) {
            throw new IllegalArgumentException("Input parameter 'regexExpr' is not found");
        }

        // The expected input schema should be at least 2 columns, (INTEGER, STRING)
        if (input.getColumnCount() < 2) {
            throw new IllegalArgumentException("Unexpected input schema, schema should be an (INTEGER, NVARCHAR or VARCHAR)");
        }

        // Check that the input column types are expected
        if (input.getColumnType(0) != java.sql.Types.INTEGER &&
                (input.getColumnType(1) != java.sql.Types.VARCHAR && input.getColumnType(1) == java.sql.Types.NVARCHAR )) {
            throw new IllegalArgumentException("Unexpected input schema, schema should be an (INTEGER, NVARCHAR or VARCHAR)");
        }
    }

    private boolean check(String text) {
        Matcher m = expr.matcher(text);

        return m.find();
    }
}
```

## Compile and create a .jar file

Package your classes and dependencies into a `.jar` files. Most Java IDEs (for example, Eclipse or IntelliJ) support generating `.jar` files when you build or compile the project. Name the `.jar` file **regex.jar**.

If you are not using a Java IDE, you can manually create a `.jar` file. For more information, see [How to create a Java jar file from class files](../how-to/create-a-java-jar-file-from-class-files.md).

> [!NOTE]
> This tutorial uses packages. The `package pkg;` line at the top of the class ensures that the compiled code is saved in a sub folder called **pkg**. If you use an IDE, the compiled code is automatically saved in this folder. If you use **javac** to manually compile the classes, you need to place the compiled code in the **pkg** folder.

## Create external language

You need to create an external language in the database. The external language is a database scoped object, which means that external languages like Java need to be created for each database you want to use it in.

### Create external language on Windows

If you are using Windows, follow the steps below to create an external language for Java.

1. Create a .zip file containing the extension.

    As part of the SQL Server setup on Windows, the Java extension **.zip** file is installed in this location: `[SQL Server install path]\MSSQL\Binn\java-lang-extension.zip`. This zip file contains the **javaextension.dll**.

2. Create an external language Java from the .zip file:

    ```sql
    CREATE EXTERNAL LANGUAGE Java
    FROM
    (CONTENT = N'[SQL Server install path]\MSSQL\Binn\java-lang-extension.zip', FILE_NAME = 'javaextension.dll',
    ENVIRONMENT_VARIABLES = N'{"JRE_HOME":"<path to JRE>"}' );
    GO
    ```

### Create external language on Linux

As part of setup, the extension **.tar.gz** file is saved under the following path:
`/opt/mssql-extensibility/lib/java-lang-extension.tar.gz`.

To create an external language Java, run the following T-SQL statement on Linux:

```sql
CREATE EXTERNAL LANGUAGE Java
FROM (CONTENT = N'/opt/mssql-extensibility/lib/java-lang-extension.tar.gz', file_name = 'javaextension.so',
ENVIRONMENT_VARIABLES = N'{"JRE_HOME":"<path to JRE>"}' );
GO
```

### Permissions to execute external language

To execute Java code, a user needs to be granted external script execution on that specific language.

For more information, see [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md).

## Create external libraries

Use [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md) to create an external library for your `.jar` files. SQL Server will have access to the `.jar` files and you do not need to set any special permissions to the **classpath**.

In this sample, you will create two external libraries. One for the SDK and one for the RegEx Java code.

1. The SDK jar file **mssql-java-lang-extension.jar** is installed as part of SQL Server 2019 on both Windows and Linux.

    + Default installation path on Windows: **[instance installation home directory]\MSSQL\Binn\mssql-java-lang-extension.jar**

    + Default installation path on Linux: **/opt/mssql/lib/mssql-java-lang-extension.jar**

    The code is also open sourced and can be found on the [SQL Server Language Extensions GitHub repository](https://github.com/microsoft/sql-server-language-extensions). For more information, see [Microsoft Extensibility SDK for Java for Microsoft SQL Server](../how-to/extensibility-sdk-java-sql-server.md).

2. Create an external library for the SDK.

    ```sql
    CREATE EXTERNAL LIBRARY sdk
    FROM (CONTENT = '<OS specific path from above>/mssql-java-lang-extension.jar')
    WITH (LANGUAGE = 'Java');
    GO
    ```

3. Create an external library for the RegEx code.

    ```sql
    CREATE EXTERNAL LIBRARY regex
    FROM (CONTENT = '<path>/regex.jar')
    WITH (LANGUAGE = 'Java');
    GO
    ```

## Set permissions

> [!NOTE]
> Skip this step, if you use external libraries in the previous step. The recommended way is to create an external library from your `.jar` file.

If you don't want to use external libraries, you will need to set the necessary permissions. Script execution only succeeds if the process identities have access to your code. You can find more information about setting permissions in  the [installation guide](../install/windows-java.md).

### On Linux

Grant read/execute permissions on the classpath to the **mssql_satellite** user.

### On Windows

Grant 'Read and Execute' permissions to **SQLRUserGroup** and the **All application packages** SID on the folder containing your compiled Java code.

The entire tree must have permissions, from root parent to the last sub folder.

1. Right-click the folder (for example, `C:\myJavaCode`) and choose **Properties** > **Security**.
2. Click **Edit**.
3. Click **Add**.
4. In **Select Users, Computer, Service Accounts, or Groups**:
   1. Click **Object Types** and make sure *Built-in security principles* and *Groups* are selected.
   2. Click **Locations** to select the local computer name at the top of the list.
5. Enter **SQLRUserGroup**, check the name, and click OK to add the group.
6. Enter **ALL APPLICATION PACKAGES**, check the name, and click OK to add. 
    If the name doesn't resolve, revisit the Locations step. The SID is local to your machine.

Make sure both security identities have **Read and Execute** permissions on the folder and the **pkg** sub folder.

<a name="call-method"></a>

## Call the Java class

Create a stored procedure that calls `sp_execute_external_script` to call the Java code from SQL Server. In the **script** parameter, define which `package.class` you want to call. In the code below, the class belongs to a package called **pkg** and a class file called **RegexSample.java**.

> [!NOTE]
> The code is not defining which method to call. By default, the **execute** method will be called. This means that you need to follow the SDK interface and implement an execute method in your Java class, if you want to be able to call the class from SQL Server.

The stored procedure takes an input query (input dataset) and a regular expression and returns the rows that fulfilled the given regular expression. It uses a regular expression `[Jj]ava` that checks if a text contains the word **Java** or **java**.

```sql
CREATE OR ALTER PROCEDURE [dbo].[java_regex] @expr nvarchar(200), @query nvarchar(400)
AS
BEGIN
--Call the Java program by giving the package.className in @script
--The method invoked in the Java code is always the "execute" method
EXEC sp_execute_external_script
  @language = N'Java'
, @script = N'pkg.RegexSample'
, @input_data_1 = @query
, @params = N'@regexExpr nvarchar(200)'
, @regexExpr = @expr
with result sets ((ID int, text nvarchar(100)));
END
GO

--Now execute the above stored procedure and provide the regular expression and an input query
EXECUTE [dbo].[java_regex] N'[Jj]ava', N'SELECT id, text FROM testdata'
GO
```

### Results

After executing the call, you should get a result set with two of the rows.

![Results from Java sample](../media/java/java-sample-results.png "Sample results")

### If you get an error

+ When you compile your classes, the **pkg** sub folder should contain the compiled code for all three classes.

+ If you are not using external libraries, check permissions on *each* folder, from the **root** to **pkg** sub folder, to ensure that the security identities running the external process have permission to read and execute your code.

## Next steps

+ [How to call Java in SQL Server](../how-to/call-java-from-sql.md)
+ [Java extensions in SQL Server](../language-extensions-overview.md)
+ [Java and SQL Server data types](../how-to/java-to-sql-data-types.md)