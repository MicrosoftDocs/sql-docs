---
title: Java language extension in SQL Server 2019 | Microsoft Docs
description: Run Java code on SQL Server 2019 using the Java language extension.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/24/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---

# SQL Server Java sample

This example demonstrates a Java class that receives two columns (ID and text) from SQL Server and returns two columns back to SQL Server (ID and ngram). For a given ID and string combination, the code generates permutations of ngrams (substrings), returning those permutations along with the original ID. The length of the ngram is defined by a parameter sent to the Java class.

## Prerequisites

+ SQL Server 2019 Database Engine instance with the extensibility framework and Java programming extension [on Windows](../install/sql-machine-learning-services-windows-install.md) or [on Linux](https://docs.microsoft.com/sql/linux/sql-server-linux-setup).

  For more information on system configuration, see [Java language extension in SQL Server 2019](extension-java.md). For more information about coding requirements, see [How to call Java in SQL Server](howto-call-java-from-sql.md).

+ Java SE Development Kit (JDK) 1.10 on Windows, or JDK 1.8 on Linux.

+ SQL Server Management Studio or another tool for running T-SQL.

A Java IDE is helpful for creating and compiling classes. If you don't have one, we recommend [Visual Studio Code](https://code.visualstudio.com/download) with the [Java extension](https://code.visualstudio.com/docs/languages/java) (not related to the SQL Server Java extension).

## 1 - Load sample data

First, create and populate a *reviews* table with **ID** and **text** columns. Connect to SQL Server and run the following script to create a table:

```sql
DROP TABLE IF exists reviews;
GO
CREATE TABLE reviews(
	id int NOT NULL,
	"text" nvarchar(30) NOT NULL)

INSERT INTO reviews(id, "text") VALUES (1, 'AAA BBB CCC DDD EEE FFF')
INSERT INTO reviews(id, "text") VALUES (2, 'GGG HHH III JJJ KKK LLL')
INSERT INTO reviews(id, "text") VALUES (3, 'MMM NNN OOO PPP QQQ RRR')
GO
```

## 2 - Class Ngram.java

Start by creating the main class. This is the first of three classes.

In this step, create a class called **Ngram.java** and copy the following Java code into that file. 


```java
//We will package our classes in a package called pkg
//Packages are option in Java-SQL, but required for this sample.
package pkg;

import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class Ngram {

    //Required: This is only required if you are passing data in @input_data_1
    //from SQL Server in sp_execute_external_script
    public static int[] inputDataCol1 = new int[1];
    public static String[] inputDataCol2 = new String[1];

    //Required: Input null map. Size just needs to be set to "1"
    public static boolean[][] inputNullMap = new boolean[1][1];

    //Required: Output data columns returned back to SQL Server
    public static int[] outputDataCol1;
    public static String[] outputDataCol2;

    //Required: Output null map. Is populated with true or false values 
    //to indicate nulls
    public static boolean[][] outputNullMap;

    //Optional: This is only required if parameters are passed with @params
    // from SQL Server in sp_execute_external_script
    // n is giving us the size of ngram substrings
    public static int param1; //= 3;

    //The number of rows we will be returning
    public static int numberOfRows;

    //Required: Number of output columns returned
    public static short numberOfOutputCols;

    //Java main method
    public static void main(String... args) {
        //getNGrams();
    }

    //This is the method we will be calling from SQL Server
    public static void getNGrams() {

        System.out.println("inputDataCol1.length= "+ inputDataCol1.length);
        if (inputDataCol1.length == 0 ) {
            // TODO: Set empty return
            return;
        }
        //Using a stream to "loop" over the input data inputDataCol1.length. You can also use a for loop for this.
        final List<InputRow> inputDataSet = IntStream.range(0, inputDataCol1.length)
                .mapToObj(i -> new InputRow(inputDataCol1[i], inputDataCol2[i]))
                .collect(Collectors.toList());


        //Again, we are using a stream to loop over data
        final List<OutputRow> outputDataSet = inputDataSet.stream()
                // Generate ngrams of size n for each incoming string
                // Each invocation of ngrams returns a list. flatMap flattens
                // the resulting list-of-lists to a flat list.
                //.flatMap(inputRow -> ngrams(param1, inputRow.text.trim()).stream()
                .flatMap(inputRow -> ngrams(param1, inputRow.text).stream().map(s -> new OutputRow(inputRow.id, s)))
                .collect(Collectors.toList());

        //Print the outputDataSet
        System.out.println(outputDataSet);

        //Set the number of rows and columns we will be returning
        numberOfOutputCols = 2;
        numberOfRows = outputDataSet.size();
        outputDataCol1 = new int[numberOfRows]; // ID column
        outputDataCol2 = new String[numberOfRows]; //The ngram column
        outputNullMap = new boolean[2][numberOfRows];// output null map

        //Since we don't have any null values, we will populate all values in the outputNullMap to false
        IntStream.range(0, numberOfRows).forEach(i -> {
            final OutputRow outputRow = outputDataSet.get(i);
            outputDataCol1[i] = outputRow.id;
            outputDataCol2[i] = outputRow.ngram;
            outputNullMap[0][i] = false;
            outputNullMap[1][i] = false;
        });
    }

    // Example: ngrams(3, "abcde") = ["abc", "bcd", "cde"].
    private static List<String> ngrams(int n, String text) {
        return IntStream.range(0, text.length() - n + 1)
                .mapToObj(i -> text.substring(i, i + n))
                .collect(Collectors.toList());
    }
```

## 3 - Class InputRow.java

Create a second class called **InputRow.java**, composed of the following code, and saved to the same location as **Ngram.java**.

```java
package pkg;

//This object represents one input row
public class InputRow {
    public final int id;
    public final String text;

    public InputRow(final int id, final String text) {
        this.id = id;
        this.text = text;
    }
}
```

## 4 - Class OutputRow.java

The third and final class is called **OutputRow.java**. Copy the code into the class and save it in the same location as the others.

```java
package pkg;

//This object represents one output row
public class OutputRow {
    public final int id;
    public final String ngram;

    public OutputRow(final int id, final String ngram) {
        this.id = id;
        this.ngram = ngram;
    }

    @Override
    public String toString() { return id + ":" + ngram; }
}
```

## 5 - Compile

Once you have your classes ready, compile them to get ".class" files. You should have three .class files for this sample. (Ngram.class, InputRow.class and OutputRow.class).

The files should be located in a subfolder called "pkg" in your classpath location. For example, if the classpath location is called '/home/myclasspath/', then the .class files should be in '/home/myclasspath/pkg'.

In this sample, the CLASSPATH provided in the sp_execute_external_script is '/home/myclasspath/'.

### Using jar files

If you plan to package your classes and dependencies into .jar files, you can do so and provide the full path to the .jar file in the sp_execute_external_script CLASSPATH parameter.

For example, if the jar file is called 'ngram.jar', the CLASSPATH will be '/home/myclasspath/ngram.jar'

<a name="call-method"></a>

## 6 - Call *getNgrams()*

To call the code from SQL Server, specify the Java method *getNgrams()* from the "script" parameter of sp_execute_external_script. This method belongs to a package called "pkg" and a class file called **Ngram.java**.

This example passes the CLASSPATH parameter to provide the path to the Java files. It also uses "params" to pass a parameter to the Java class. 

Run the following code in SQL Server Management Studio or another tool used for running Transact-SQL.

```sql
DECLARE @myClassPath nvarchar(30)
DECLARE @n int 
--This is where you store your classes or jars.
--Update this to your own classpath
SET @myClassPath = N'/home/myclasspath/'
--This is the size of the ngram
SET @n = 3
EXEC sp_execute_external_script
  @language = N'Java'
, @script = N'pkg.Ngram.getNGrams'
, @input_data_1 = N'SELECT id, text FROM reviews'
, @parallel = 0
, @params = N'@CLASSPATH nvarchar(30), @param1 INT'
, @CLASSPATH = @myClassPath
, @param1 = @n
with result sets ((ID int, ngram varchar(20)))
GO
```

### Results

After executing the call, you should receive a result set showing the two columns:

![Results from Java sample](../media/java/java-sample-results.png "Sample results")

## See also

+ [How to call Java in SQL Server](howto-call-java-from-sql.md)
+ [Java extensions in SQL Server](extension-java.md)
+ [Java and SQL Server data types](java-sql-datatypes.md)