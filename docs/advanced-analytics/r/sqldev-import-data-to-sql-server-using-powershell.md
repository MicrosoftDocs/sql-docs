---
title: Lesson 2 Prepare R tutorial environment using PowerShell (SQL Server Machine Learning) | Microsoft Docs
description: Tutorial showing how to embed R in SQL Server stored procedures and T-SQL functions 
ms.prod: sql
ms.technology: machine-learning

ms.date: 06/07/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Lesson 2: Set up the tutorial environment using PowerShell
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is part of a tutorial for SQL developers on how to use R in SQL Server.

In this step, you'll run a PowerShell script to create the database objects required for the walkthrough. The script creates and loads a database using sample data obtained in the previous step. It also creates functions and stored procedures used throughout the tutorial.

## Create and load database objects

Among the downloaded files, you should see a PowerShell script (`RunSQL_SQL_Walkthrough.ps1`) that prepares the environment for the walkthrough. Actions performed by the script include:

- Install the SQL Native Client and SQL command-line utilities, if not already installed. These utilities are required for bulk-loading the data to the database using **bcp**.

- Create a database and tables on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, and bulk-insert data sourced from a .csv file.

- Create multiple SQL functions and stored procedures.

### Modify the script to use a trusted Windows identity

By default, the script assumes a SQL Server database user login and password. If you are db_owner under your Windows user account, you can use your Windows identity to create the objects. To do so, open `RunSQL_SQL_Walkthrough.ps1` in a code editor and append **`-T`** to the bcp bulk insert command (line 238):

```text
bcp $db_tb in $csvfilepath -t ',' -S $server -f taxiimportfmt.xml -F 2 -C "RAW" -b 200000 -U $u -P $p -T
```

### Run the script to create objects

Open a PowerShell command prompt as administrator and run the following command.
  
```ps
.\RunSQL_SQL_Walkthrough.ps1
```
You are prompted to input the following information:

- Server instance where [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] has been installed. On a default instance, this can be as simple as the machine name.

- Database name. For this tutorial, scripts assume `TaxiNYC_Sample`.

- User name and user password. Enter a SQL Server database login for these values. Alternatively, if you modified the script to accept a trusted Windows identity, press Enter to leave these values blank. Your Windows identity is used on the connection.

- Fully qualified file name for the sample data downloaded in the previous lesson. For example: `C:\tempRSQL\nyctaxi1pct.csv`

After you provide these values, the script executes immediately. During script execution, all placeholder names in the [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts are updated to use the inputs you provide.

## Review database objects
   
When script execution is finished, confirm the database objects exist on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You should see the database, tables, functions, and stored procedures.
  
   ![rsql_devtut_BrowseTables](media/rsql-devtut-browsetables.png "rsql_devtut_BrowseTables")

> [!NOTE]
> If the database objects already exist, they cannot be created again.
>   
> If the table already exists, the data will be appended, not overwritten. Therefore, be sure to drop any existing objects before running the script.

## Objects used in this tutorial

The following table summarizes the objects created in this lesson. Although you only run one PowerShell script (`RunSQL_SQL_Walkthrough.ps1`), that script calls other SQL scripts in turn to create the objects in your database. Scripts used to create each object are mentioned in the description.

|**Object name**|**Object type**|**Description**|
|----------|------------------------|---------------|
|**TaxiNYC_Sample** | database |Created by the create-db-tb-upload-data.sql script. Creates a database and two tables:<br /><br />dbo.nyctaxi_sample table: Contains the main NYC Taxi dataset. A clustered columnstore index is added to the table to improve storage and query performance. The 1% sample of the NYC Taxi dataset is inserted into this table.<br /><br />dbo.nyc_taxi_models table: Used to persist the trained advanced analytics model.|
|**fnCalculateDistance** |scalar-valued function | Created by the fnCalculateDistance.sql script. Calculates the direct distance between pickup and dropoff locations. This function is used in [Create data features](../tutorials/sqldev-create-data-features-using-t-sql.md), [Train and save a model](sqldev-train-and-save-a-model-using-t-sql.md)  and [Operationalize the R model](../tutorials/sqldev-operationalize-the-model.md).|
|**fnEngineerFeatures** |table-valued function | Created by the fnEngineerFeatures.sql script. Creates new data features for model training. This function is used in [Create data features](../tutorials/sqldev-create-data-features-using-t-sql.md) and [Operationalize the R model](../tutorials/sqldev-operationalize-the-model.md).|
|**PlotHistogram** |stored procedure | Created by the PlotHistogram.sql script. Calls an R function to plot the histogram of a variable and then returns the plot as a binary object. This stored procedure is used in [Explore and visualize data](../tutorials/sqldev-explore-and-visualize-the-data.md).|
|**PlotInOutputFiles** |stored procedure| Created by the  PlotInOutputFiles.sql script. Creates a graphic using an R function and then saves the output as a local PDF file. This stored procedure is used in [Explore and visualize data](../tutorials/sqldev-explore-and-visualize-the-data.md).|
|**PersistModel** |stored procedure | Created by the PersistModel.sql script. Takes a model that has been serialized in a varbinary data type, and writes it to the specified table. |
|**PredictTip**  |stored procedure |Created by the PredictTip.sql script. Calls the trained model to create predictions using the model. The stored procedure accepts a query as its input parameter and returns a column of numeric values containing the scores for the input rows. This stored procedure is used in [Operationalize the R model](../tutorials/sqldev-operationalize-the-model.md).|
|**PredictTipSingleMode**  |stored procedure| Created by the PredictTipSingleMode.sql script. Calls the trained model to create predictions using the model. This stored procedure accepts a new observation as input, with individual feature values passed as in-line parameters, and returns a value that predicts the outcome for the new observation. This stored procedure is used in [Operationalize the R model](../tutorials/sqldev-operationalize-the-model.md).|
|**TrainTipPredictionModel**  |stored procedure|Created by the TrainTipPredictionModel.sql script. Trains a logistic regression model by calling an R package. The model predicts the value of the  tipped column, and is trained using a randomly selected 70% of the data. The output of the stored procedure is the trained model, which is saved in the table nyc_taxi_models. This stored procedure is used in [Train and save a model](sqldev-train-and-save-a-model-using-t-sql.md).|

## Next lesson

[Lesson 3: Explore and visualize the data](../tutorials/sqldev-explore-and-visualize-the-data.md)

## Previous lesson

[Lesson 1: Download the sample data](../tutorials/sqldev-download-the-sample-data.md)
