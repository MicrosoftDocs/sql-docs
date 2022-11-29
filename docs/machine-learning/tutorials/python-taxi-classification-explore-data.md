---
title: "Python tutorial: Explore and visualize data"
titleSuffix: SQL machine learning
description: Explore sample data and generate some plots in preparation for using binary classification in Python with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning

ms.date: 09/17/2021
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||>=azuresqldb-mi-current"
---

# Python tutorial: Explore and visualize data
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

In part two of this five-part tutorial series, you'll explore the sample data and generate some plots. Later, you'll learn how to serialize graphics objects in Python, and then deserialize those objects and make plots.

In this article, you'll:

> [!div class="checklist"]
> + Review the sample data
> + Create plots using Python in T-SQL

In [part one](python-taxi-classification-introduction.md), you installed the prerequisites and restored the sample database.

In [part three](python-taxi-classification-create-features.md), you'll learn how to create features from raw data by using a Transact-SQL function. You'll then call that function from a stored procedure to create a table that contains the feature values.

In [part four](python-taxi-classification-train-model.md), you'll load the modules and call the necessary functions to create and train the model using a SQL Server stored procedure.

In [part five](python-taxi-classification-deploy-model.md), you'll learn how to operationalize the models that you trained and saved in part four.

## Review the data

First, take a minute to browse the data schema, as we've made some changes to make it easier to use the NYC Taxi data

+ The original dataset used separate files for the taxi identifiers and trip records. We've joined the two original datasets on the columns _medallion_, _hack_license_, and _pickup_datetime_.  
+ The original dataset spanned many files and was quite large. We've downsampled to get just 1% of the original number of records. The current data table has 1,703,957 rows and 23 columns.

**Taxi identifiers**

The _medallion_ column represents the taxi's unique ID number.

The _hack_license_ column contains the taxi driver's license number (anonymized).

**Trip and fare records**

Each trip record includes the pickup and drop-off location and time, and the trip distance.

Each fare record includes payment information such as the payment type, total amount of payment, and the tip amount.

The last three columns can be used for various machine learning tasks.  The _tip_amount_ column contains continuous numeric values and can be used as the **label** column for regression analysis. The _tipped_ column has only yes/no values and is used for binary classification. The _tip_class_ column has multiple **class labels** and therefore can be used as the label for multi-class classification tasks.

The values used for the label columns are all based on the `tip_amount` column, using these business rules:

+ Label column `tipped` has possible values 0 and 1

    If `tip_amount` > 0, `tipped` = 1; otherwise `tipped` = 0

+ Label column `tip_class` has possible class values 0-4

    Class 0: `tip_amount` = $0

    Class 1: `tip_amount` > $0 and `tip_amount` <= $5
    
    Class 2: `tip_amount` > $5 and `tip_amount` <= $10
    
    Class 3: `tip_amount` > $10 and `tip_amount` <= $20
    
    Class 4: `tip_amount` > $20

## Create plots using Python in T-SQL

Developing a data science solution usually includes intensive data exploration and data visualization. Because visualization is such a powerful tool for understanding the distribution of the data and outliers, Python provides many packages for visualizing data. The **matplotlib** module is one of the more popular libraries for visualization, and includes many functions for creating histograms, scatter plots, box plots, and other data exploration graphs.

In this section, you learn how to work with plots using stored procedures. Rather than open the image on the server, you store the Python object  `plot` as **varbinary** data, and then write that to a file that can be shared or viewed elsewhere.

### Create a plot as varbinary data

The stored procedure returns a serialized Python `figure` object as a stream of **varbinary** data. You cannot view the binary data directly, but you can use Python code on the client to deserialize and view the figures, and then save the image file on a client computer.

1. Create the stored procedure **PyPlotMatplotlib**.

   In the following script:
    - The variable `@query` defines the query text `SELECT tipped FROM nyctaxi_sample`, which is passed to the Python code block as the argument to the script input variable, `@input_data_1`.
    - The Python script is fairly simple: **matplotlib** `figure` objects are used to make the histogram and scatter plot, and these objects are then serialized using the `pickle` library.
    - The Python graphics object is serialized to a **pandas** DataFrame for output.
  
    ```sql
    DROP PROCEDURE IF EXISTS PyPlotMatplotlib;
    GO

    CREATE PROCEDURE [dbo].[PyPlotMatplotlib]
    AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @query nvarchar(max) =
        N'SELECT cast(tipped as int) as tipped, tip_amount, fare_amount FROM [dbo].[nyctaxi_sample]'
        EXECUTE sp_execute_external_script
        @language = N'Python',
        @script = N'
    import matplotlib
    matplotlib.use("Agg")
    import matplotlib.pyplot as plt
    import pandas as pd
    import pickle

    fig_handle = plt.figure()
    plt.hist(InputDataSet.tipped)
    plt.xlabel("Tipped")
    plt.ylabel("Counts")
    plt.title("Histogram, Tipped")
    plot0 = pd.DataFrame(data =[pickle.dumps(fig_handle)], columns =["plot"])
    plt.clf()

    plt.hist(InputDataSet.tip_amount)
    plt.xlabel("Tip amount ($)")
    plt.ylabel("Counts")
    plt.title("Histogram, Tip amount")
    plot1 = pd.DataFrame(data =[pickle.dumps(fig_handle)], columns =["plot"])
    plt.clf()

    plt.hist(InputDataSet.fare_amount)
    plt.xlabel("Fare amount ($)")
    plt.ylabel("Counts")
    plt.title("Histogram, Fare amount")
    plot2 = pd.DataFrame(data =[pickle.dumps(fig_handle)], columns =["plot"])
    plt.clf()

    plt.scatter( InputDataSet.fare_amount, InputDataSet.tip_amount)
    plt.xlabel("Fare Amount ($)")
    plt.ylabel("Tip Amount ($)")
    plt.title("Tip amount by Fare amount")
    plot3 = pd.DataFrame(data =[pickle.dumps(fig_handle)], columns =["plot"])
    plt.clf()

    OutputDataSet = plot0.append(plot1, ignore_index=True).append(plot2, ignore_index=True).append(plot3, ignore_index=True)
    ',
    @input_data_1 = @query
    WITH RESULT SETS ((plot varbinary(max)))
    END
    GO
    ```

2. Now run the stored procedure with no arguments to generate a plot from the data hard-coded as the input query.

    ```sql
    EXEC [dbo].[PyPlotMatplotlib]
    ```

3. The results should be something like this:
  
	```sql
    plot
    0xFFD8FFE000104A4649...
	0xFFD8FFE000104A4649...
	0xFFD8FFE000104A4649...
	0xFFD8FFE000104A4649...
    ```

4. From a [Python client](../python/setup-python-client-tools-sql.md), you can now connect to the SQL Server instance that generated the binary plot objects, and view the plots. 

    To do this, run the following Python code, replacing the server name, database name, and credentials as appropriate (for Windows authentication, replace the `UID` and `PWD` parameters with `Trusted_Connection=True`). Make sure the Python version is the same on the client and the server. Also make sure that the Python libraries on your client (such as matplotlib) are the same or higher version relative to the libraries installed on the server. To view a list of installed packages and their versions, see [Get Python package information](../package-management/python-package-information.md#list-all-installed-python-packages).
  
    ```python
    %matplotlib notebook
    import pyodbc
    import pickle
    import os
    cnxn = pyodbc.connect('DRIVER=SQL Server;SERVER={SERVER_NAME};DATABASE={DB_NAME};UID={USER_NAME};PWD={PASSWORD}')
    cursor = cnxn.cursor()
    cursor.execute("EXECUTE [dbo].[PyPlotMatplotlib]")
    tables = cursor.fetchall()
    for i in range(0, len(tables)):
        fig = pickle.loads(tables[i][0])
        fig.savefig(str(i)+'.png')
    print("The plots are saved in directory: ",os.getcwd())
    ```

5. If the connection is successful, you should see a message like the following:
  
   *The plots are saved in directory: xxxx*
  
6. The output file is created in the Python working directory. To view the plot, locate the Python working directory, and open the file. The following image shows a plot saved on the client computer.
  
   ![Tip amount vs Fare amount](media/sqldev-python-sample-plot.png "Tip amount vs Fare amount") 

## Next steps

In this article, you:

> [!div class="checklist"]
> + Reviewed the sample data
> + Created plots using Python in T-SQL

> [!div class="nextstepaction"]
> [Python tutorial: Create Data Features using T-SQL](python-taxi-classification-create-features.md)