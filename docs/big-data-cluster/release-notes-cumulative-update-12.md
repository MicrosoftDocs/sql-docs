---
title: SQL Server Big Data Clusters CU12 release notes
titleSuffix: SQL Server Big Data Clusters
description: This article describes the SQL Server Big Data Clusters Cumulative Update 12 contents.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 08/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# SQL Server Big Data Clusters CU12 release notes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] cumulative update 12 (CU12).

## Tested configurations for CU12

SQL Server Big Data Clusters CU12 was tested on the following environment combinations:

| Container OS | Kubernetes API | Runtime | Data Storage | Log Storage |
| ------------ | ------- | ------- | ------------ | ----------- |
| Ubuntu 20.04 LTS | 1.20 | containerd 1.4.3<br/>docker 20.10.2<br/>CRI-O 1.20.0 | Block only | Block only |

Reference Architecture White Papers for SQL Server Big Data Clusters can be found on the following pages:

* https://www.microsoft.com/sql-server/sql-server-2019
* [SQL Server Big Data Clusters partners](partner-big-data-cluster.md)

## System environment

* __Operating System__: Ubuntu 20.04.2 LTS
* __SQL Server__: 15.0.4153.13
    * __Java__: Azul Zulu JRE 11.0.9.1
    * __Python__: 3.7.2 (miniconda 4.5.12)
    * __R__: Microsoft R 3.5.2
* __Spark__: 2.4
    * __Java__: Azul Zulu JRE 1.8.0_275
    * __Scala__: 2.11
    * __Python__: 3.7.2 (miniconda 4.5.12)
    * __R__: Microsoft R 3.5.2
    * __Spark SQL Connector__: [1.0.2](https://github.com/microsoft/sql-spark-connector)

## Embedded OSS component versions

| Component | Version |
|--|--|
| [collectd](https://collectd.org/) | 5.12 |
| [InfluxDB](https://www.influxdata.com) | 1.8.3 |
| [Elasticsearch](https://www.elastic.co/) | 7.9.1 |
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) | 1.10.1.0 |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) | 1.6.3 |
| [Grafana](https://grafana.com/) | 7.3.6 |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) | 3.1 |
| [Hive (Metastore)](https://hive.apache.org/) | 2.3 |
| [Kibana](https://www.elastic.co/kibana) | 7.9.1 |
| [Knox](https://knox.apache.org/) | 1.4.0 |
| [Livy](https://livy.apache.org/) | 0.6 |
| [Openresty (Nginx)](https://openresty.org/) | 1.17.8-2 |
| [Spark](configure-spark-hdfs.md) | 2.4 |
| [Telegraf](https://docs.influxdata.com/telegraf/) | 1.16.1 |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) | 3.5.8 |

## Installed Python libraries

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
| revoscalepy | 9.4.7 | pycurl | 7.43.0.2 | jupyter-client | 5.2.4 |
| microsoftml | 9.4.7 | pycrypto | 2.6.1 | jsonschema | 2.6.0 |
| zipp | 0.5.2 | pycparser | 2.19 | Jinja2 | 2.10.1 |
| zict | 1.0.0 | pycosat | 0.6.3 | jedi | 0.15.1 |
| xlwt | 1.3.0 | py | 1.8.0 | jdcal | 1.4 |
| XlsxWriter | 1.1.2 | ptyprocess | 0.6.0 | itsdangerous | 1.1.0 |
| xlrd | 1.2.0 | psutil | 5.4.8 | ipywidgets | 7.4.2 |
| wrapt | 1.11.2 | prompt-toolkit | 2.0.7 | ipython | 7.2.0 |
| widgetsnbextension | 3.4.2 | prometheus-client | 0.7.1 | ipython-genutils | 0.2.0 |
| wheel | 0.33.4 | pluggy | 0.12.0 | ipykernel | 4.9.0 |
| Werkzeug | 0.15.5 | plotly | 4.0.0 | importlib-metadata | 0.19 |
| webencodings | 0.5.1 | pkginfo | 1.5.0.1 | imagesize | 1.1.0 |
| wcwidth | 0.1.7 | pip | 18.1 | imageio | 2.5.0 |
| urllib3 | 1.24.2 | Pillow | 6.1.0 | idna | 2.8 |
| unicodecsv | 0.14.1 | pickleshare | 0.7.5 | heapdict | 1.0.0 |
| traitlets | 4.3.2 | pexpect | 4.7.0 | hdijupyterutils | 0.12.9 |
| tornado | 6.0.3 | patsy | 0.5.1 | h5py | 2.9.0 |
| toolz | 0.9.0 | pathlib2 | 2.3.3 | gmpy2 | 2.0.8 |
| testpath | 0.4.2 | path.py | 11.5.0 | Flask | 1.1.1 |
| terminado | 0.8.2 | partd | 0.3.9 | Flask-Cors | 3.0.8 |
| tblib | 1.4.0 | parso | 0.5.1 | fastcache | 1.1.0 |
| tables | 3.5.2 | pandocfilters | 1.4.2 | et-xmlfile | 1.0.1 |
| sympy | 1.3 | pandasql | 0.7.3 | entrypoints | 0.3 |
| statsmodels | 0.9.0 | pandas | 0.23.4 | docutils | 0.15.2 |
| sqlparse | 0.2.4 | pandas-datareader | 0.7.0 | distributed | 1.28.1 |
| SQLAlchemy | 1.2.15 | packaging | 19.1 | dill | 0.2.8.2 |
| sphinxcontrib-websupport | 1.1.2 | openpyxl | 2.5.12 | defusedxml | 0.6.0 |
| Sphinx | 1.8.2 | olefile | 0.46 | decorator | 4.4.0 |
| sparkmagic | 0.12.6 | odo | 0.5.1 | datashape | 0.5.4 |
| sortedcontainers | 2.1.0 | numpydoc | 0.8.0 | dask | 1.2.2 |
| snowballstemmer | 1.9.0 | numpy | 1.15.4 | cytoolz | 0.9.0.1 |
| six | 1.12.0 | numexpr | 2.6.9 | Cython | 0.29.2 |
| setuptools | 41.0.1 | numba | 0.42.0 | cycler | 0.10.0 |
| Send2Trash | 1.5.0 | notebook | 5.7.8 | cryptography | 2.5 |
| seaborn | 0.9.0 | nltk | 3.4 | configobj | 5.0.6 |
| scipy | 1.1.0 | networkx | 2.2 | conda | 4.5.12 |
| scikit-learn | 0.20.2 | nbformat | 4.4.0 | cloudpickle | 0.6.1 |
| scikit-image | 0.14.1 | nbconvert | 5.5.0 | Click | 7.0 |
| ruamel-yaml | 0.15.46 | multipledispatch | 0.6.0 | chest | 0.2.3 |
| retrying | 1.3.3 | msgpack | 0.6.1 | chardet | 3.0.4 |
| requests | 2.21.0 | mpmath | 1.1.0 | cffi | 1.12.3 |
| requests-kerberos | 0.12.0 | more-itertools | 7.2.0 | certifi | 2019.6.16 |
| qtconsole | 4.5.3 | mock | 3.0.5 | Bottleneck | 1.2.1 |
| pyzmq | 18.1.0 | mkl-random | 1.0.2 | bokeh | 1.0.3 |
| PyYAML | 5.1.2 | mkl-fft | 1.0.10 | bleach | 3.1.0 |
| PyWavelets | 1.0.3 | mistune | 0.8.4 | blaze | 0.11.3 |
| pytz | 2019.2 | matplotlib | 3.0.2 | backports.os | 0.1.1 |
| python-dateutil | 2.8.0 | MarkupSafe | 1.1.0 | backcall | 0.1.0 |
| pytest | 4.0.2 | lxml | 4.3.0 | Babel | 2.7.0 |
| PySocks | 1.7.0 | locket | 0.2.0 | azureml-model-management-sdk | 1.0.1b10 |
| pyparsing | 2.4.2 | llvmlite | 0.27.0 | autovizwidget | 0.12.9 |
| pyOpenSSL | 19.0.0 | liac-arff | 2.4.0 | attrs | 19.1.0 |
| pyodbc | 4.0.25 | kiwisolver | 1.1.0 | atomicwrites | 1.3.0 |
| pykerberos | 1.2.1 | jupyter | 1.0.0 | asn1crypto | 0.24.0 |
| PyJWT | 1.7.1 | jupyter-core | 4.5.0 | alabaster | 0.7.12 |
| Pygments | 2.4.2 | jupyter-console | 6.0.0 | adal | 1.2.2 |

## Installed R libraries

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
CompatibilityAPI | 1.1.0 | crayon | 1.3.4 | plogr | 0.2.0 |
MicrosoftML | 9.4.7 | curl | 3.3 | plyr | 1.8.4 |
RevoPemaR | 10.0.0 | datasets | 3.5.2 | png | 0.1-7 |
RevoScaleR | 9.4.7 | dbplyr | 1.2.2 | promises | 1.0.1 |
RevoTreeView | 10.0.0 | deployrRserve | 9.0.0 | psych | 1.8.10 |
doRSR | 10.0.0 | digest | 0.6.18 | purrr | 0.2.5 |
mrsdeploy | 1.1.3 | doParallel | 1.0.14 | r2d3 | 0.2.3 |
sqlrutils | 1.0.0 | dplyr | 0.7.8 | rappdirs | 0.3.1 |
BH | 1.69.0-1 | fansi | 0.4.0 | readr | 1.3.1 |
DBI | 1.0.0 | foreach | 1.5.1 | reshape2 | 1.4.3 |
KernSmooth | 2.23-15 | foreign | 0.8-71 | rlang | 0.3.1 |
MASS | 7.3-51.1 | forge | 0.1.0 | rpart | 4.1-13 |
Matrix | 1.2-15 | generics | 0.0.2 | rprojroot | 1.3-2 |
MicrosoftR | 3.5.2 | glue | 1.3.0 | rstudioapi | 0.9.0 |
R6 | 2.3.0 | grDevices | 3.5.2 | shiny | 1.2.0 |
RUnit | 0.4.26 | graphics | 3.5.2 | sourcetools | 0.1.7 |
Rcpp | 1.0.0 | grid | 3.5.2 | sparklyr | 0.9.4 |
RevoIOQ | 10.0.1 | hms | 0.4.2 | spatial | 7.3-11 |
RevoMods | 11.0.1 | htmltools | 0.3.6 | splines | 3.5.2 |
RevoUtils | 11.0.2 | htmlwidgets | 1.3 | stats | 3.5.2 |
RevoUtilsMath | 11.0.0 | httpuv | 1.4.5.1 | stats4 | 3.5.2 |
askpass | 1.1 | httr | 1.4.0 | stringi | 1.2.4 |
assertthat | 0.2.0 | iterators | 1.0.11 | stringr | 1.3.1 |
backports | 1.1.3 | jsonlite | 1.5 | survival | 2.43-3 |
base | 3.5.2 | later | 0.7.5 | sys | 2.1 |
base64enc | 0.1-3 | lattice | 0.20-38 | tcltk | 3.5.2 |
bindr | 0.1.1 | lazyeval | 0.2.1 | tibble | 2.0.1 |
bindrcpp | 0.2.2 | magrittr | 1.5 | tidyr | 0.8.2 |
boot | 1.3-20 | methods | 3.5.2 | tidyselect | 0.2.5 |
broom | 0.5.1 | mgcv | 1.8-26 | tools | 3.5.2 |
checkpoint | 0.4.4 | mime | 0.6 | utf8 | 1.1.4 |
class | 7.3-14 | mnormt | 1.5-5 | utils | 3.5.2 |
cli | 1.0.1 | nlme | 3.1-137 | withr | 2.1.2 |
clipr | 0.5.0 | nnet | 7.3-12 | xml2 | 1.2.0 |
cluster | 2.0.7-1 | openssl | 1.2.1 | xtable | 1.8-3 |
codetools | 0.2-15 | parallel | 3.5.2 | yaml | 2.2.0 |
compiler | 3.5.2 | pillar | 1.3.1 | | |
config | 0.3 | pkgconfig | 2.0.2 | | |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
