---
title: "EXPLAIN (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: conceptual
ms.assetid: 4846a576-57ea-4068-959c-81e69e39ddc1
author: shkale-msft
ms.author: shkale
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# EXPLAIN (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Returns the query plan for a [!INCLUDE[ssDW](../../includes/ssdw-md.md)] [!INCLUDE[DWsql](../../includes/dwsql-md.md)] statement without running the statement. Use **EXPLAIN** to preview which operations will require data movement and to view the estimated costs of the query operations.  
  
 For more information about query plans, see "Understanding Query Plans" in the [!INCLUDE[pdw-product-documentation_md](../../includes/pdw-product-documentation-md.md)].  
  
## Syntax  
  

```  
EXPLAIN SQL_statement  
[;]  
```  
  
## Arguments  
 *SQL_statement*  
 The [!INCLUDE[DWsql](../../includes/dwsql-md.md)] statement on which **EXPLAIN** will run. *SQL_statement* can be any of these commands: **SELECT**, **INSERT**, **UPDATE**, **DELETE**, **CREATE TABLE AS SELECT**, **CREATE REMOTE TABLE**.  
  
## Permissions  
 Requires the **SHOWPLAN** permission, and permission to execute *SQL_statement*. See [Permissions: GRANT, DENY, REVOKE &#40;Azure SQL Data Warehouse, Parallel Data Warehouse&#41;](../../t-sql/statements/permissions-grant-deny-revoke-azure-sql-data-warehouse-parallel-data-warehouse.md).  
  
## Return Value  
 The return value from the **EXPLAIN** command is an XML document with the structure shown below. This XML document lists all operations in the query plan for the given query, each enclosed by the `<dsql_operation>` tag. The return value is of type **nvarchar(max)**.  
  
 The returned query plan depicts sequential SQL statements; when the query runs it may involve parallelized operations, so some of the sequential statements shown may run at the same time.  
  
```  
\<?xml version="1.0" encoding="utf-8"?>  
<dsql_query>  
  <sql>. . .</sql>  
  <params />  
  <dsql_operations>  
    <dsql_operation>  
     . . .      
    </dsql_operation>  
    [ . . . n ]  
  <dsql_operations>  
</dsql_query>  
```  
  
 The XML tags contain this information:  
  
|XML Tag|Summary, Attributes, and Content|  
|-------------|--------------------------------------|  
|\<dsql_query>|Top level/document element.|
|\<sql>|Echoes *SQL_statement*.|  
|\<params>|This tag is not used at this time.|  
|\<dsql_operations>|Summarizes and contains the query steps, and includes cost information for the query. Also contains all of the `<dsql_operation>` blocks. This tag contains count information for the entire query:<br /><br /> `<dsql_operations total_cost=total_cost total_number_operations=total_number_operations>`<br /><br /> *total_cost* is the total estimated time for the query to run, in ms.<br /><br /> *total_number_operations* is the total number of operations for the query. An operation that will be parallelized and run on multiple nodes is counted as a single operation.|  
|\<dsql_operation>|Describes a single operation within the query plan. The \<dsql_operation> tag contains the operation type as an attribute:<br /><br /> `<dsql_operation operation_type=operation_type>`<br /><br /> *operation_type* is one of the values found in [sys.dm_pdw_request_steps (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql.md).<br /><br /> The content in the `\<dsql_operation>` block is dependent on the operation type.<br /><br /> See the table below.|  
  
|Operation Type|Content|Example|  
|--------------------|-------------|-------------|  
|BROADCAST_MOVE, DISTRIBUTE_REPLICATED_TABLE_MOVE, MASTER_TABLE_MOVE, PARTITION_MOVE, SHUFFLE_MOVE, and TRIM_MOVE|`<operation_cost>` element, with these attributes. Values reflect only the local operation:<br /><br /> -   *cost* is the local operator cost and shows the estimated time for the operation to run, in ms.<br />-   *accumulative_cost* is the sum of all seen operations in the plan including summed values for parallel operations, in ms.<br />-   *average_rowsize* is the estimated average row size (in bytes) of rows retrieved and passed during the operation.<br />-   *output_rows* is the output (node) cardinality and shows the number of output rows.<br /><br /> `<location>`: The nodes or distributions where the operation will occur. Options are: "Control", "ComputeNode", "AllComputeNodes", "AllDistributions", "SubsetDistributions", "Distribution", and "SubsetNodes".<br /><br /> `<source_statement>`: The source data for the shuffle move.<br /><br /> `<destination_table>`: The internal temporary table the data will be moved into.<br /><br /> `<shuffle_columns>`: (Applicable only to SHUFFLE_MOVE operations). One or more columns that will be used as the distribution columns for the temporary table.|`<operation_cost cost="40" accumulative_cost="40" average_rowsize = "50" output_rows="100"/>`<br /><br /> `<location distribution="AllDistributions" />`<br /><br /> `<source_statement type="statement">SELECT [TableAlias_3b77ee1d8ccf4a94ba644118b355db9d].[dist_date] FROM [qatest].[dbo].[flyers] [TableAlias_3b77ee1d8ccf4a94ba644118b355db9d]       </source_statement>`<br /><br /> `<destination_table>Q_[TEMP_ID_259]_[PARTITION_ID]</destination_table>`<br /><br /> `<shuffle_columns>dist_date;</shuffle_columns>`|  
|CopyOperation|`<operation_cost>`: See `<operation_cost>` above.<br /><br /> `<DestinationCatalog>`: The destination node or nodes.<br /><br /> `<DestinationSchema>`: The destination schema in DestinationCatalog.<br /><br /> `<DestinationTableName>`: Name of the destination table or "TableName".<br /><br /> `<DestinationDatasource>`: The name or connection information for the destination datasource.<br /><br /> `<Username>` and `<Password>`: These fields indicate that a username and password for the destination may be required.<br /><br /> `<BatchSize>`: The batch size for the copy operation.<br /><br /> `<SelectStatement>`: The select statement used to perform the copy.<br /><br /> `<distribution>`: The distribution where the copy is performed.|`<operation_cost cost="0" accumulative_cost="0" average_rowsize="4" output_rows="1" />`<br /><br /> `<DestinationCatalog>master</DestinationCatalog>`<br /><br /> `<DestinationSchema>dbo</DestinationSchema>`<br /><br /> `<DestinationTableName>[TableName]</DestinationTableName>`<br /><br /> `<DestinationDatasource>localhost, 8080</DestinationDatasource>`<br /><br /> `<Username>...</Username>`<br /><br /> `<Password>...</Password>`<br /><br /> `<BatchSize>6000</BatchSize>`<br /><br /> `<SelectStatement>SELECT T1_1.c1 AS c1 FROM [qatest].[dbo].[gigs] AS T1_1</SelectStatement>`<br /><br /> `<distribution>ControlNode</distribution>`|  
|MetaDataCreate_Operation|`<source_table>`: The source table for the operation.<br /><br /> `<destination_table>`: The destination table for the operation.|`<source_table>databases</source_table>`<br /><br /> `<destination_table>MetaDataCreateLandingTempTable</destination_table>`|  
|ON|`<location>`: See `<location>` above.<br /><br /> `<sql_operation>`: Identifies the SQL command that will be performed on a node.|`<location permanent="false" distribution="AllDistributions">Compute</location>`<br /><br /> `<sql_operation type="statement">CREATE TABLE [tempdb].[dbo]. [Q_[TEMP_ID_259]]_ [PARTITION_ID]]]([dist_date] DATE) WITH (DISTRIBUTION = HASH([dist_date]),) </sql_operation>`|  
|RemoteOnOperation|`<DestinationCatalog>`: The destination catalog.<br /><br /> `<DestinationSchema>`: The destination schema in DestinationCatalog.<br /><br /> `<DestinationTableName>`: Name of the destination table or "TableName".<br /><br /> `<DestinationDatasource>`: Name of the destination datasource.<br /><br /> `<Username>` and `<Password>`: These fields indicate that a username and password for the destination may be required.<br /><br /> `<CreateStatement>`: The table creation statement for the destination database.|`<DestinationCatalog>master</DestinationCatalog>`<br /><br /> `<DestinationSchema>dbo</DestinationSchema>`<br /><br /> `<DestinationTableName>TableName</DestinationTableName>`<br /><br /> `<DestinationDatasource>DestDataSource</DestinationDatasource>`<br /><br /> `<Username>...</Username>`<br /><br /> `<Password>...</Password>`<br /><br /> `<CreateStatement>CREATE TABLE [master].[dbo].[TableName] ([col1] BIGINT) ON [PRIMARY] WITH(DATA_COMPRESSION=PAGE);</CreateStatement>`|  
|RETURN|`<resultset>`: The identifier for the result set.|`<resultset>RS_19</resultset>`|  
|RND_ID|`<identifier>`: The identifier for the object created.|`<identifier>TEMP_ID_260</identifier>`|  
  
## Limitations and Restrictions  
 **EXPLAIN** can be applied to *optimizable* queries only, which are queries that can be improved or modified based on the results of an **EXPLAIN** command. The supported **EXPLAIN** commands are listed above. Attempting to use **EXPLAIN** with an unsupported query type will either return an error or echo the query.  
  
 **EXPLAIN** is not supported in a user transaction.  
  
## Examples  
 The following example shows an **EXPLAIN** command run on a **SELECT** statement, and the XML result.  
  
 **Submitting an EXPLAIN statement**  
  
 The submitted command for this example is:  
  
```  
-- Uses AdventureWorks  
  
EXPLAIN   
    SELECT CAST (AVG(YearlyIncome) AS int) AS AverageIncome,   
        CAST(AVG(FIS.SalesAmount) AS int) AS AverageSales,   
        G.StateProvinceName, T.SalesTerritoryGroup  
    FROM dbo.DimGeography AS G  
    JOIN dbo.DimSalesTerritory AS T  
        ON G.SalesTerritoryKey = T.SalesTerritoryKey  
    JOIN dbo.DimCustomer AS C  
        ON G.GeographyKey = C.GeographyKey  
    JOIN dbo.FactInternetSales AS FIS  
        ON C.CustomerKey = FIS.CustomerKey  
    WHERE T.SalesTerritoryGroup IN ('North America', 'Pacific')  
        AND Gender = 'F'  
    GROUP BY G.StateProvinceName, T.SalesTerritoryGroup  
    ORDER BY AVG(YearlyIncome) DESC;  
GO  
```  
  
 After executing the statement using the **EXPLAIN** option, the message tab presents a single line titled **explain**, and starting with the XML text `\<?xml version="1.0" encoding="utf-8"?>` Click on the XML to open the entire text in an XML window. To better understand the following comments, you should turn on the display of line numbers in SSDT.  
  
#### To turn on line numbers  
  
1.  With the output appearing in the **explain** tab SSDT, on the **TOOLS** menu, select **Options**.  
  
2.  Expand the **Text Editor** section, expand **XML**, and then click **General**.  
  
3.  In the **Display** area, check **Line numbers**.  
  
4.  Click **OK**.  
  
 **Example EXPLAIN output**  
  
 The XML result of the **EXPLAIN** command with row numbers turned on is:  
  
```  
1  \<?xml version="1.0" encoding="utf-8"?>  
2  <dsql_query>  
3    <sql>SELECT CAST (AVG(YearlyIncome) AS int) AS AverageIncome,   
4          CAST(AVG(FIS.SalesAmount) AS int) AS AverageSales,   
5          G.StateProvinceName, T.SalesTerritoryGroup  
6      FROM dbo.DimGeography AS G  
7      JOIN dbo.DimSalesTerritory AS T  
8          ON G.SalesTerritoryKey = T.SalesTerritoryKey  
9      JOIN dbo.DimCustomer AS C  
10          ON G.GeographyKey = C.GeographyKey  
11      JOIN dbo.FactInternetSales AS FIS  
12          ON C.CustomerKey = FIS.CustomerKey  
13      WHERE T.SalesTerritoryGroup IN ('North America', 'Pacific')  
14          AND Gender = 'F'  
15      GROUP BY G.StateProvinceName, T.SalesTerritoryGroup  
16      ORDER BY AVG(YearlyIncome) DESC</sql>  
17    <dsql_operations total_cost="0.926237696" total_number_operations="9">  
18      <dsql_operation operation_type="RND_ID">  
19        <identifier>TEMP_ID_16893</identifier>  
20      </dsql_operation>  
21      <dsql_operation operation_type="ON">  
22        <location permanent="false" distribution="AllComputeNodes" />  
23        <sql_operations>  
24          <sql_operation type="statement">CREATE TABLE [tempdb].[dbo].[TEMP_ID_16893] ([CustomerKey] INT NOT NULL, [GeographyKey] INT, [YearlyIncome] MONEY ) WITH(DATA_COMPRESSION=PAGE);</sql_operation>  
25        </sql_operations>  
26      </dsql_operation>  
27      <dsql_operation operation_type="BROADCAST_MOVE">  
28        <operation_cost cost="0.121431552" accumulative_cost="0.121431552" average_rowsize="16" output_rows="31.6228" />  
29        <source_statement>SELECT [T1_1].[CustomerKey] AS [CustomerKey],  
30         [T1_1].[GeographyKey] AS [GeographyKey],  
31         [T1_1].[YearlyIncome] AS [YearlyIncome]  
32  FROM   (SELECT [T2_1].[CustomerKey] AS [CustomerKey],  
33                 [T2_1].[GeographyKey] AS [GeographyKey],  
34                 [T2_1].[YearlyIncome] AS [YearlyIncome]  
35          FROM   [AdventureWorksPDW2012].[dbo].[DimCustomer] AS T2_1  
36          WHERE  ([T2_1].[Gender] = CAST (N'F' COLLATE Latin1_General_100_CI_AS_KS_WS AS NVARCHAR (1)) COLLATE Latin1_General_100_CI_AS_KS_WS)) AS T1_1</source_statement>  
37        <destination_table>[TEMP_ID_16893]</destination_table>  
38      </dsql_operation>  
39      <dsql_operation operation_type="RND_ID">  
40        <identifier>TEMP_ID_16894</identifier>  
41      </dsql_operation>  
42      <dsql_operation operation_type="ON">  
43        <location permanent="false" distribution="AllDistributions" />  
44        <sql_operations>  
45          <sql_operation type="statement">CREATE TABLE [tempdb].[dbo].[TEMP_ID_16894] ([StateProvinceName] NVARCHAR(50) COLLATE Latin1_General_100_CI_AS_KS_WS, [SalesTerritoryGroup] NVARCHAR(50) COLLATE Latin1_General_100_CI_AS_KS_WS NOT NULL, [col] BIGINT, [col1] MONEY NOT NULL, [col2] BIGINT, [col3] MONEY NOT NULL ) WITH(DATA_COMPRESSION=PAGE);</sql_operation>  
46        </sql_operations>  
47      </dsql_operation>  
48      <dsql_operation operation_type="SHUFFLE_MOVE">  
49        <operation_cost cost="0.804806144" accumulative_cost="0.926237696" average_rowsize="232" output_rows="108.406" />  
50        <source_statement>SELECT [T1_1].[StateProvinceName] AS [StateProvinceName],  
51         [T1_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup],  
52         [T1_1].[col2] AS [col],  
53         [T1_1].[col] AS [col1],  
54         [T1_1].[col3] AS [col2],  
55         [T1_1].[col1] AS [col3]  
56  FROM   (SELECT ISNULL([T2_1].[col1], CONVERT (MONEY, 0.00, 0)) AS [col],  
57                 ISNULL([T2_1].[col3], CONVERT (MONEY, 0.00, 0)) AS [col1],  
58                 [T2_1].[StateProvinceName] AS [StateProvinceName],  
59                 [T2_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup],  
60                 [T2_1].[col] AS [col2],  
61                 [T2_1].[col2] AS [col3]  
62          FROM   (SELECT   COUNT_BIG([T3_2].[YearlyIncome]) AS [col],  
63                           SUM([T3_2].[YearlyIncome]) AS [col1],  
64                           COUNT_BIG(CAST ((0) AS INT)) AS [col2],  
65                           SUM([T3_2].[SalesAmount]) AS [col3],  
66                           [T3_2].[StateProvinceName] AS [StateProvinceName],  
67                           [T3_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup]  
68                  FROM     (SELECT [T4_1].[SalesTerritoryKey] AS [SalesTerritoryKey],  
69                                   [T4_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup]  
70                            FROM   [AdventureWorksPDW2012].[dbo].[DimSalesTerritory] AS T4_1  
71                            WHERE  (([T4_1].[SalesTerritoryGroup] = CAST (N'North America' COLLATE Latin1_General_100_CI_AS_KS_WS AS NVARCHAR (13)) COLLATE Latin1_General_100_CI_AS_KS_WS)  
72                                    OR ([T4_1].[SalesTerritoryGroup] = CAST (N'Pacific' COLLATE Latin1_General_100_CI_AS_KS_WS AS NVARCHAR (7)) COLLATE Latin1_General_100_CI_AS_KS_WS))) AS T3_1  
73                           INNER JOIN  
74                           (SELECT [T4_1].[SalesTerritoryKey] AS [SalesTerritoryKey],  
75                                   [T4_2].[YearlyIncome] AS [YearlyIncome],  
76                                   [T4_2].[SalesAmount] AS [SalesAmount],  
77                                   [T4_1].[StateProvinceName] AS [StateProvinceName]  
78                            FROM   [AdventureWorksPDW2012].[dbo].[DimGeography] AS T4_1  
79                                   INNER JOIN  
80                                   (SELECT [T5_2].[GeographyKey] AS [GeographyKey],  
81                                           [T5_2].[YearlyIncome] AS [YearlyIncome],  
82                                           [T5_1].[SalesAmount] AS [SalesAmount]  
83                                    FROM   [AdventureWorksPDW2012].[dbo].[FactInternetSales] AS T5_1  
84                                           INNER JOIN  
85                                           [tempdb].[dbo].[TEMP_ID_16893] AS T5_2  
86                                           ON ([T5_1].[CustomerKey] = [T5_2].[CustomerKey])) AS T4_2  
87                                   ON ([T4_2].[GeographyKey] = [T4_1].[GeographyKey])) AS T3_2  
88                           ON ([T3_1].[SalesTerritoryKey] = [T3_2].[SalesTerritoryKey])  
89                  GROUP BY [T3_2].[StateProvinceName], [T3_1].[SalesTerritoryGroup]) AS T2_1) AS T1_1</source_statement>  
90        <destination_table>[TEMP_ID_16894]</destination_table>  
91        <shuffle_columns>StateProvinceName;</shuffle_columns>  
92      </dsql_operation>  
93      <dsql_operation operation_type="ON">  
94        <location permanent="false" distribution="AllComputeNodes" />  
95        <sql_operations>  
96          <sql_operation type="statement">DROP TABLE [tempdb].[dbo].[TEMP_ID_16893]</sql_operation>  
97        </sql_operations>  
98      </dsql_operation>  
99      <dsql_operation operation_type="RETURN">  
100        <location distribution="AllDistributions" />  
101        <select>SELECT   [T1_1].[col] AS [col],  
102           [T1_1].[col1] AS [col1],  
103           [T1_1].[StateProvinceName] AS [StateProvinceName],  
104           [T1_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup],  
105           [T1_1].[col2] AS [col2]  
106  FROM     (SELECT CONVERT (INT, [T2_1].[col], 0) AS [col],  
107                   CONVERT (INT, [T2_1].[col1], 0) AS [col1],  
108                   [T2_1].[StateProvinceName] AS [StateProvinceName],  
109                   [T2_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup],  
110                   [T2_1].[col] AS [col2]  
111            FROM   (SELECT CASE  
112                            WHEN ([T3_1].[col] = CAST ((0) AS BIGINT)) THEN CAST (NULL AS MONEY)  
113                            ELSE ([T3_1].[col1] / CONVERT (MONEY, [T3_1].[col], 0))  
114                           END AS [col],  
115                           CASE  
116                            WHEN ([T3_1].[col2] = CAST ((0) AS BIGINT)) THEN CAST (NULL AS MONEY)  
117                            ELSE ([T3_1].[col3] / CONVERT (MONEY, [T3_1].[col2], 0))  
118                           END AS [col1],  
119                           [T3_1].[StateProvinceName] AS [StateProvinceName],  
120                           [T3_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup]  
121                    FROM   (SELECT ISNULL([T4_1].[col], CONVERT (BIGINT, 0, 0)) AS [col],  
122                                   ISNULL([T4_1].[col1], CONVERT (MONEY, 0.00, 0)) AS [col1],  
123                                   ISNULL([T4_1].[col2], CONVERT (BIGINT, 0, 0)) AS [col2],  
124                                   ISNULL([T4_1].[col3], CONVERT (MONEY, 0.00, 0)) AS [col3],  
125                                   [T4_1].[StateProvinceName] AS [StateProvinceName],  
126                                   [T4_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup]  
127                            FROM   (SELECT   SUM([T5_1].[col]) AS [col],  
128                                             SUM([T5_1].[col1]) AS [col1],  
129                                             SUM([T5_1].[col2]) AS [col2],  
130                                             SUM([T5_1].[col3]) AS [col3],  
131                                             [T5_1].[StateProvinceName] AS [StateProvinceName],  
132                                             [T5_1].[SalesTerritoryGroup] AS [SalesTerritoryGroup]  
133                                    FROM     [tempdb].[dbo].[TEMP_ID_16894] AS T5_1  
134                                    GROUP BY [T5_1].[StateProvinceName], [T5_1].[SalesTerritoryGroup]) AS T4_1) AS T3_1) AS T2_1) AS T1_1  
135  ORDER BY [T1_1].[col2] DESC</select>  
136      </dsql_operation>  
137      <dsql_operation operation_type="ON">  
138        <location permanent="false" distribution="AllDistributions" />  
139        <sql_operations>  
140          <sql_operation type="statement">DROP TABLE [tempdb].[dbo].[TEMP_ID_16894]</sql_operation>  
141        </sql_operations>  
142      </dsql_operation>  
143    </dsql_operations>  
144  </dsql_query>  
  
```  
  
 **Meaning of the EXPLAIN output**  
  
 The output above contains 144 numbered lines. Your output from this query may differ slightly. The following list describes significant sections.  
  
-   Lines 3 through 16 provide a description of the query that is being analyzed.  
  
-   Line 17, specifies that the total number of operations will be 9. You can find the start of each operation, by looking for the words **dsql_operation**.  
  
-   Line 18 starts operation 1. Lines 18 and 19 indicate that a **RND_ID** operation will create a random ID number that will be used for an object description. The object described in the output above is **TEMP_ID_16893**. Your number will be different.  
  
-   Line 20 starts operation 2. Lines 21 through 25: On all compute nodes, create a temporary table named **TEMP_ID_16893**.  
  
-   Line 26 starts operation 3. Lines 27 through 37: Move data to **TEMP_ID_16893** by using a broadcast move. The query sent to each compute node is provided. Line 37 specifies the destination table is **TEMP_ID_16893**.  
  
-   Line 38 starts operation 4. Lines 39 through 40: Create a random ID for a table. **TEMP_ID_16894** is the ID number in the example above. Your number will be different.  
  
-   Line 41 starts operation 5. Lines 42 through 46: On all nodes, create a temporary table named **TEMP_ID_16894**.  
  
-   Line 47 starts operation 6. Lines 48 through 91: Move data from various tables (including **TEMP_ID_16893**) to table **TEMP_ID_16894**, by using a shuffle move operation. The query sent to each compute node is provided. Line 90 specifies the destination table as **TEMP_ID_16894**. Line 91 specifies the columns.  
  
-   Line 92 starts operation 7. Lines 93 through 97: On all compute nodes, drop temporary table **TEMP_ID_16893**.  
  
-   Line 98 starts operation 8. Lines 99 through 135: Return results to the client. Uses the query provided to get the results.  
  
-   Line 136 starts operation 9. Lines 137 through 140: On all nodes, drop temporary table **TEMP_ID_16894**.  
  
  

