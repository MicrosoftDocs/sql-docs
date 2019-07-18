
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-
ssver15-xxxx-xxxx-xxx.md)] 

The following article provides reference for the **sql** commands in the 
**azdata** tool. For more information about other **azdata** commands, see 
[azdata reference](reference-azdata.md)

# azdata bdc pool status
## Commands
|     |     |
| --- | --- |
[azdata bdc pool status show](#azdata-bdc-pool-status-show) | Pool status.
## azdata bdc pool status show
Pool status.
```bash
azdata bdc pool status show --kind -k 
                            [--name -n]
```
### Examples
Get status of the storage pool.
```bash
azdata bdc pool status show --kind storage --name default
```
Get status of the data pool.
```bash
azdata bdc pool status show --kind data --name default
```
Get status of the compute pool.
```bash
azdata bdc pool status show --kind compute --name default
```
Get status of the master pool.
```bash
azdata bdc pool status show --kind master --name default
```
Get status of the spark pool.
```bash
azdata bdc pool status show --kind spark --name default
```
### Required Parameters
#### `--kind -k`
Big data cluster pool kind.
### Optional Parameters
#### `--name -n`
Big data cluster pool name.
`default`
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