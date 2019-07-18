
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-
ssver15-xxxx-xxxx-xxx.md)] 

The following article provides reference for the **sql** commands in the 
**azdata** tool. For more information about other **azdata** commands, see 
[azdata reference](reference-azdata.md)

# azdata bdc endpoint
## Commands
|     |     |
| --- | --- |
[azdata bdc endpoint list](#azdata-bdc-endpoint-list) | Lists the endpoints for the Big Data Cluster.
## azdata bdc endpoint list
Lists the endpoints for the Big Data Cluster.
```bash
azdata bdc endpoint list [--endpoint-name -e] 
                         
```
### Optional Parameters
#### `--endpoint-name -e`
Big data cluster endpoint name.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.