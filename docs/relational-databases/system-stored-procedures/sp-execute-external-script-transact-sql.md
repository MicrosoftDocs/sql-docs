---
title: "sp_execute_external_script (Transact-SQL)"
description: Executes a script provided as an input argument to the procedure, and is used with Machine Learning Services and Language Extensions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/30/2023
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: "reference"
f1_keywords:
  - "sp_execute_external_script_TSQL"
  - "sys.sp_execute_external_script"
  - "sys.sp_execute_external_script_TSQL"
  - "sp_execute_external_script"
helpviewer_keywords:
  - "sp_execute_external_script"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-ver15 || =azuresqldb-mi-current"
---
# sp_execute_external_script (Transact-SQL)

[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
The `sp_execute_external_script` stored procedure executes a script provided as an input argument to the procedure, and is used with [Machine Learning Services](../../machine-learning/sql-server-machine-learning-services.md) and [Language Extensions](../../language-extensions/language-extensions-overview.md).

For Machine Learning Services, [Python](../../machine-learning/concepts/extension-python.md) and [R](../../machine-learning/concepts/extension-r.md) are supported languages. For Language Extensions, Java is supported but must be defined with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md).

To execute `sp_execute_external_script`, you must first install Machine Learning Services or Language Extensions. For more information, see [Install SQL Server Machine Learning Services (Python and R) on Windows](../../machine-learning/install/sql-machine-learning-services-windows-install.md) and [Linux](../../linux/sql-server-linux-setup-machine-learning.md), or [Install SQL Server Language Extensions on Windows](../../language-extensions/install/windows-java.md) and [Linux](../../linux/sql-server-linux-setup-language-extensions-java.md).
::: moniker-end

::: moniker range="=sql-server-2017"
The `sp_execute_external_script` stored procedure executes a script provided as an input argument to the procedure, and is used with [Machine Learning Services](../../machine-learning/sql-server-machine-learning-services.md) on [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].

For Machine Learning Services, [Python](../../machine-learning/concepts/extension-python.md) and [R](../../machine-learning/concepts/extension-r.md) are supported languages.

To execute `sp_execute_external_script`, you must first install Machine Learning Services. For more information, see [Install SQL Server Machine Learning Services (Python and R) on Windows](../../machine-learning/install/sql-machine-learning-services-windows-install.md).
::: moniker-end

::: moniker range="=sql-server-2016"
The `sp_execute_external_script` stored procedure executes a script provided as an input argument to the procedure, and is used with [R Services](../../machine-learning/r/sql-server-r-services.md) on [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].

For R Services, [R](../../machine-learning/concepts/extension-r.md) is the supported language.

To execute `sp_execute_external_script`, you must first install R Services. For more information, see [Install SQL Server Machine Learning Services (Python and R) on Windows](../../machine-learning/install/sql-r-services-windows-install.md).
::: moniker-end

::: moniker range="=azuresqldb-mi-current"
The `sp_execute_external_script` stored procedure executes a script provided as an input argument to the procedure, and is used with [Machine Learning Services in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/machine-learning-services-overview).

For Machine Learning Services, [Python](../../machine-learning/concepts/extension-python.md) and [R](../../machine-learning/concepts/extension-r.md) are supported languages.

To execute `sp_execute_external_script`, you must first enable Machine Learning Services. For more information, see the [Machine Learning Services in Azure SQL Managed Instance documentation](/azure/azure-sql/managed-instance/machine-learning-services-overview).
::: moniker-end

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"

## Syntax

```syntaxsql
sp_execute_external_script
    [ @language = ] N'language'
    , [ @script = ] N'script'
    [ , [ @input_data_1 = ] N'input_data_1' ]
    [ , [ @input_data_1_name = ] N'input_data_1_name' ]
    [ , [ @input_data_1_order_by_columns = ] N'input_data_1_order_by_columns' ]
    [ , [ @input_data_1_partition_by_columns = ] N'input_data_1_partition_by_columns' ]
    [ , [ @output_data_1_name = ] N'output_data_1_name' ]
    [ , [ @parallel = ] { 0 | 1 } ]
    [ , [ @params = ] N'@parameter_name data_type [ OUT | OUTPUT ] [ , ...n ]' ]
    [ , [ @parameter1 = ] 'value1' [ OUT | OUTPUT ] [ , ...n ] ]
[ ; ]
```

::: moniker-end

::: moniker range="=sql-server-2016"

## Syntax for SQL Server 2017 and previous versions

```syntaxsql
EXEC sp_execute_external_script
    @language = N'language'
    , @script = N'script'
    [ , [ @input_data_1 = ] N'input_data_1' ]
    [ , [ @input_data_1_name = ] N'input_data_1_name' ]
    [ , [ @output_data_1_name = ] N'output_data_1_name' ]
    [ , [ @parallel = ] { 0 | 1 } ]
    [ , [ @params = ] N'@parameter_name data_type [ OUT | OUTPUT ] [ ,...n ]' ]
    [ , [ @parameter1 = ] 'value1' [ OUT | OUTPUT ] [ ,...n ] ]
```

::: moniker-end

## Arguments

#### [ @language = ] N'*language*'

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
Indicates the script language. *language* is **sysname**. Valid values are **R**, **Python**, and any language defined with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md) (for example, Java).
::: moniker-end
::: moniker range="=sql-server-2017"
Indicates the script language. *language* is **sysname**. In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], valid values are **R** and **Python**.
::: moniker-end
::: moniker range="=sql-server-2016"
Indicates the script language. *language* is **sysname**. In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the only valid value is **R**.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
Indicates the script language. *language* is **sysname**. In Azure SQL Managed Instance, valid values are **R** and **Python**.
::: moniker-end

#### [ @script = ] N'*script*'

External language  script specified as a literal or variable input. *script* is **nvarchar(max)**.

#### [ @input_data_1 = ] N'*input_data_1*'

Specifies the input data used by the external script in the form of a [!INCLUDE [tsql](../../includes/tsql-md.md)] query. The data type of *input_data_1* is **nvarchar(max)**.

#### [ @input_data_1_name = ] N'*input_data_1_name*'

Specifies the name of the variable used to represent the query defined by @input_data_1. The data type of the variable in the external script depends on the language. For R, the input variable is a data frame. For Python, input must be tabular. *input_data_1_name* is **sysname**. Default value is *InputDataSet*.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"

#### [ @input_data_1_order_by_columns = ] N'input_data_1_order_by_columns'

Used to build per-partition models. Specifies the name of the column used to order the result set, for example by product name. The data type of the variable in the external script depends on the language. For R, the input variable is a data frame. For Python, input must be tabular.

#### [ @input_data_1_partition_by_columns = ] N'*input_data_1_partition_by_columns*'

Used to build per-partition models. Specifies the name of the column used to segment data, such as geographic region or date. The data type of the variable in the external script depends on the language. For R, the input variable is a data frame. For Python, input must be tabular.

::: moniker-end

#### [ @output_data_1_name = ] N'*output_data_1_name*'

Specifies the name of the variable in the external script that contains the data to be returned to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] upon completion of the stored procedure call. The data type of the variable in the external script depends on the language. For R, the output must be a data frame. For Python, the output must be a pandas data frame. *output_data_1_name* is **sysname**. Default value is *OutputDataSet*.

#### [ @parallel = ] { 0 | 1 }

Enable parallel execution of R scripts by setting the `@parallel` parameter to `1`. The default for this parameter is `0` (no parallelism). If `@parallel = 1` and the output is being streamed directly to the client machine, then the `WITH RESULT SETS` clause is required and an output schema must be specified.

- For R scripts that don't use RevoScaleR functions, using the  `@parallel` parameter can be beneficial for processing large datasets, assuming the script can be trivially parallelized. For example, when using the R `predict` function with a model to generate new predictions, set `@parallel = 1` as a hint to the query engine. If the query can be parallelized, rows are distributed according to the **MAXDOP** setting.

- For R scripts that use RevoScaleR functions, parallel processing is handled automatically and you shouldn't specify `@parallel = 1` to the `sp_execute_external_script` call.

#### [ @params = ] N'*@parameter_name data_type*' [ OUT | OUTPUT ] [ ,...n ]

A list of input parameter declarations that are used in the external script.

#### [ @parameter1 = ] '*value1*' [ OUT | OUTPUT ] [ ,...n ]

A list of values for the input parameters used by the external script.

## Remarks

> [!IMPORTANT]  
> The query tree is controlled by SQL machine learning and users can't perform arbitrary operations on the query.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
Use `sp_execute_external_script` to execute scripts written in a supported language. Supported languages are **Python** and **R** used with Machine Learning Services, and any language defined with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md) (for example, Java) used with Language Extensions.
::: moniker-end
::: moniker range="=sql-server-2017"
Use `sp_execute_external_script` to execute scripts written in a supported language. Supported languages are **Python** and **R** in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Machine Learning Services.
::: moniker-end
::: moniker range="=sql-server-2016"
Use `sp_execute_external_script` to execute scripts written in a supported language. The only supported language is **R** in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
Use `sp_execute_external_script` to execute scripts written in a supported language. Supported languages are **Python** and **R** in Azure SQL Managed Instance Machine Learning Services.
::: moniker-end

By default, result sets returned by this stored procedure are output with unnamed columns. Column names used within a script are local to the scripting environment and aren't reflected in the outputted result set. To name result set columns, use the `WITH RESULT SET` clause of [`EXECUTE`](../../t-sql/language-elements/execute-transact-sql.md).

In addition to returning a result set, you can return scalar values to using OUTPUT parameters.

::: moniker range=">=sql-server-2016||>=sql-server-linux-ver15"
You can control the resources used by external scripts by configuring an external resource pool. For more information, see [CREATE EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-external-resource-pool-transact-sql.md). Information about the workload can be obtained from the resource governor catalog views, DMV's, and counters. For more information, see [Resource Governor Catalog Views (Transact-SQL)](../system-catalog-views/resource-governor-catalog-views-transact-sql.md), [Resource Governor Related Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md), and [SQL Server, External Scripts Object](../performance-monitor/sql-server-external-scripts-object.md).  
::: moniker-end

### Monitor script execution

Monitor script execution using [sys.dm_external_script_requests](../system-dynamic-management-views/sys-dm-external-script-requests.md) and [sys.dm_external_script_execution_stats](../system-dynamic-management-views/sys-dm-external-script-execution-stats.md).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"

### Parameters for partition modeling

You can set two additional parameters that enable modeling on partitioned data, where partitions are based on one or more columns you provide that naturally segment a data set into logical partitions, created and used only during script execution. Columns containing repeating values for age, gender, geographic region, date or time, are a few examples that lend themselves to partitioned data sets.

The two parameters are **input_data_1_partition_by_columns** and **input_data_1_order_by_columns**, where the second parameter is used to order the result set. The parameters are passed as inputs to `sp_execute_external_script` with the external script executing once for every partition. For more information and examples, see [Tutorial: Create partition-based models](../../machine-learning/tutorials/r-tutorial-create-models-per-partition.md).

You can execute script in parallel by specifying `@parallel = 1`. If the input query can be parallelized, you should set `@parallel = 1` as part of your arguments to `sp_execute_external_script`. By default, the query optimizer operates under `@parallel = 1` on tables having more than 256 rows, but if you want to handle this explicitly, this script includes the parameter as a demonstration.

> [!TIP]  
> For training workoads, you can use `@parallel` with any arbitrary training script, even those using non-Microsoft-rx algorithms. Typically, only RevoScaleR algorithms (with the rx prefix) offer parallelism in training scenarios in SQL Server. But with the new parameters in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, you can parallelize a script that calls functions not specifically engineered with that capability.
::: moniker-end

### Streaming execution for Python and R scripts

Streaming allows the Python or R script to work with more data than can fit in memory. To control the number of rows passed during streaming, specify an integer value for the parameter, `@r_rowsPerRead` in the `@params` collection. For example, if you're training a model that uses very wide data, you could adjust the value to read fewer rows, to ensure that all rows can be sent in one chunk of data. You might also use this parameter to manage the number of rows being read and processed at one time, to mitigate server performance issues.

Both the `@r_rowsPerRead` parameter for streaming and the `@parallel` argument should be considered hints. For the hint to be applied, it must be possible to generate a SQL query plan that includes parallel processing. If this isn't possible, parallel processing can't be enabled.

> [!NOTE]  
> Streaming and parallel processing are supported only in Enterprise Edition. You can include the parameters in your queries in Standard Edition without raising an error, but the parameters have no effect and R scripts run in a single process.

## Limitations

### Data types

The following data types aren't supported when used in the input query or parameters of `sp_execute_external_script` procedure, and return an unsupported type error.

As a workaround, `CAST` the column or value to a supported type in [!INCLUDE [tsql](../../includes/tsql-md.md)] before sending it to the external script.

- **cursor**
- **timestamp**
- **datetime2**, **datetimeoffset**, **time**
- **sql_variant**
- **text**, **image**
- **xml**
- **hierarchyid**, **geometry**, **geography**
- CLR user-defined types

In general, any result set that can't be mapped to a [!INCLUDE [tsql](../../includes/tsql-md.md)] data type, is output as `NULL`.

### Restrictions specific to R

If the input includes **datetime** values that don't fit the permissible range of values in R, values are converted to `NA`. This is required because SQL machine learning permits a larger range of values than is supported in the R language.

Float values (for example, `+Inf`, `-Inf`, `NaN`) aren't supported in SQL machine learning even though both languages use IEEE 754. Current behavior just sends the values to SQL directly; as a result, the SQL client throws an error. Therefore, these values are converted to `NULL`.

## Permissions

Requires EXECUTE ANY EXTERNAL SCRIPT database permission.

## Examples

This section contains examples of how this stored procedure can be used to execute R or Python scripts using [!INCLUDE [tsql](../../includes/tsql-md.md)].

### A. Return an R data set to SQL Server

The following example creates a stored procedure that uses `sp_execute_external_script` to return the Iris dataset included with R.

```sql
DROP PROCEDURE IF EXISTS get_iris_dataset;
GO
CREATE PROCEDURE get_iris_dataset
AS
BEGIN
    EXEC sp_execute_external_script @language = N'R',
        @script = N'iris_data <- iris;',
        @input_data_1 = N'',
        @output_data_1_name = N'iris_data'
    WITH RESULT SETS((
        "Sepal.Length" FLOAT NOT NULL,
        "Sepal.Width" FLOAT NOT NULL,
        "Petal.Length" FLOAT NOT NULL,
        "Petal.Width" FLOAT NOT NULL,
        "Species" VARCHAR(100)
    ));
END;
GO
```

### B. Create a Python model and generate scores from it

This example illustrates how to use `sp_execute_external_script` to generate scores on a simple Python model.

```sql
CREATE PROCEDURE [dbo].[py_generate_customer_scores]
AS
BEGIN
    -- Input query to generate the customer data
    DECLARE @input_query NVARCHAR(MAX) = N'SELECT customer, orders, items, cost FROM dbo.Sales.Orders'

    EXEC sp_execute_external_script @language = N'Python',
        @script = N'
import pandas as pd
from sklearn.cluster import KMeans

# Get data from input query
customer_data = my_input_data

# Define the model
n_clusters = 4
est = KMeans(n_clusters=n_clusters, random_state=111).fit(customer_data[["orders","items","cost"]])
clusters = est.labels_
customer_data["cluster"] = clusters

OutputDataSet = customer_data
',
        @input_data_1 = @input_query,
        @input_data_1_name = N'my_input_data'
    WITH RESULT SETS((
        "CustomerID" INT,
        "Orders" FLOAT,
        "Items" FLOAT,
        "Cost" FLOAT,
        "ClusterResult" FLOAT
    ));
END;
GO
```

Column headings used in Python code aren't output to SQL Server; therefore, use the WITH RESULT statement to specify the column names and data types for SQL to use.

::: moniker range=">=sql-server-2016||>=sql-server-linux-ver15"

### C. Generate an R model based on data from SQL Server

The following example creates a stored procedure that uses `sp_execute_external_script` to generate an iris model and return the model.

> [!NOTE]  
> This example requires advance installation of the **e1071** package. For more information, see [Install additional R packages on SQL Server](../../machine-learning/package-management/install-additional-r-packages-on-sql-server.md).

```sql
DROP PROCEDURE IF EXISTS generate_iris_model;
GO
CREATE PROCEDURE generate_iris_model
AS
BEGIN
    EXEC sp_execute_external_script @language = N'R',
        @script = N'
      library(e1071);
      irismodel <-naiveBayes(iris_data[,1:4], iris_data[,5]);
      trained_model <- data.frame(payload = as.raw(serialize(irismodel, connection=NULL)));
',
        @input_data_1 = N'select "Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species" from iris_data',
        @input_data_1_name = N'iris_data',
        @output_data_1_name = N'trained_model'
    WITH RESULT SETS((model VARBINARY(MAX)));
END;
GO
```

To generate a similar model using Python, you would change the language identifier from `@language=N'R'` to `@language = N'Python'`, and make necessary modifications to the `@script` argument. Otherwise, all parameters function the same way as for R.

::: moniker-end

For scoring, you can also use the native [PREDICT](../../t-sql/queries/predict-transact-sql.md) function, which is typically faster because it avoids calling the Python or R runtime.

## Related content

- [SQL machine learning](../../machine-learning/index.yml)
- [SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [CREATE EXTERNAL LIBRARY (Transact-SQL)](../../t-sql/statements/create-external-library-transact-sql.md)
- [sp_prepare (Transact SQL)](sp-prepare-transact-sql.md)
- [sp_configure (Transact-SQL)](sp-configure-transact-sql.md)
- [external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)
- [SERVERPROPERTY (Transact-SQL)](../../t-sql/functions/serverproperty-transact-sql.md)
- [SQL Server, External Scripts Object](../performance-monitor/sql-server-external-scripts-object.md)
- [sys.dm_external_script_requests](../system-dynamic-management-views/sys-dm-external-script-requests.md)
- [sys.dm_external_script_execution_stats](../system-dynamic-management-views/sys-dm-external-script-execution-stats.md)
