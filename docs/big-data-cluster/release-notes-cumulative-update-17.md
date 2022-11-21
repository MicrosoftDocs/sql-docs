---
title: SQL Server 2019 Big Data Clusters CU17 release notes
titleSuffix: SQL Server Big Data Clusters
description: This article describes the SQL Server 2019 Big Data Clusters Cumulative Update 17 contents.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 09/13/2022
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# SQL Server 2019 Big Data Clusters CU17 release notes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] cumulative update 17 (CU17).

## CU17 changes and new capabilities

   > [!WARNING]
   > On Cumulative Update 15, __the upgrade order is critical__. Upgrade your big data cluster to CU15 __before__ upgrading the Kubernetes cluster to version 1.21. If the Kubernetes cluster is upgraded to version 1.21 before BDC is upgraded to CU14 or CU15 then the cluster will end up in error state and the BDC upgrade will not succeed. In this case, reverting back to Kubernetes version 1.20 will fix the problem.
   > <br/> This __doesn't affect new deployments__ of SQL Server 2019 Big Data Clusters CU15 on Kubernetes API 1.21 clusters.

SQL Server Big Data Clusters CU17 includes important changes and capabilities. The following known issue has been resolved: 

- When you use the `AZDATA BDC ROTATE` command to rotate the password of a big data cluster that uses Active Directory, you receive the following error message: `Failed to update password for existing AD account '<Account Name>'. Error code: 30`. This issue has been resolved.

For detailed SQL Server engine changes, check the [official SQL Server 2019 CU17 knowledge base article KB5016394](https://support.microsoft.com/topic/kb5016394-cumulative-update-17-for-sql-server-2019-3033f654-b09d-41aa-8e49-e9d0c353c5f7).

## Tested configurations for CU17

[!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] CU17 was tested on the following environment combinations:

| Container OS | Kubernetes API | Runtime | Data Storage | Log Storage |
| ------------ | ------- | ------- | ------------ | ----------- |
| Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br/>CRI-O 1.20.4 | Block only | Block only |

Reference Architecture White Papers for [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] can be found on the following pages:

* [SQL Server 2019](https://www.microsoft.com/sql-server/sql-server-2019)
* [SQL Server 2019 Big Data Clusters partners](partner-big-data-cluster.md)

## System environment

* __Operating System__: Ubuntu 20.04.5 LTS
* __SQL Server__: 15.0.4249.2
    * __Java__: Azul Zulu JRE 11.0.9.1
    * __Python__: 3.7.2 (miniconda 4.5.12)
    * __R__: Microsoft R 3.5.2
* __runtime for Apache Spark 2022.1__
    * __Spark__: 3.1.2
    * __Delta Lake__: 1.0.0
    * __Java__: Azul Zulu JRE 1.8.0_275
    * __Scala__: 2.12
    * __Python__: 3.8 (miniforge 4.9)
    * __R__: CRAN R 4.1.2
    * __Spark SQL Connector__: [1.2.0](https://github.com/microsoft/sql-spark-connector)

## Embedded OSS component versions

| Component | Version |
|--|--|
| [collectd](https://collectd.org/) | 5.12.0 |
| [InfluxDB](https://www.influxdata.com) | 1.8.3 |
| [Elasticsearch](https://www.elastic.co/) | 7.9.1 |
| [opendistro-elasticsearch-security](https://www.elastic.co/what-is/elastic-stack-security) | 1.10.1.0 |
| [Fluent Bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) | 1.6.3 |
| [Grafana](https://grafana.com/) | 7.3.6 |
| Hadoop <br/>[HDFS DataNode](concept-storage-pool.md)<br/>[HDFS NameNode](https://cwiki.apache.org/confluence/display/HADOOP2/NameNode) | 3.1 |
| [Hive (Metastore)](https://hive.apache.org/) | 2.3 |
| [Kibana](https://www.elastic.co/kibana) | 7.9.1 |
| [Knox](https://knox.apache.org/) | 1.4.0 |
| [Livy](https://livy.apache.org/) | 0.7 |
| [Openresty (Nginx)](https://openresty.org/) | 1.17.8-2 |
| [Spark](configure-spark-hdfs.md) | 3.1.2 |
| [Telegraf](https://docs.influxdata.com/telegraf/) | 1.16.1 |
| [ZooKeeper](https://cwiki.apache.org/confluence/display/zookeeper) | 3.5.8 |

## Runtime for Apache Spark release 2022.1 (BDC.3.2022.1) - Installed Python libraries

| Library | Version | Library | Version | Library | Version |
|--|--|--|--|--|--|
|    _libgcc_mutex    |    0.1    |    keras-applications    |    1.0.8    |    python    |    3.8.10    |
|    _openmp_mutex    |    4.5    |    keras-preprocessing    |    1.1.2    |    pytorch    |    1.8.1    |
|    _py-xgboost-mutex    |    2    |    keras2onnx    |    1.6.5    |    pyqt    |    5.12.3    |
|    abseil-cpp    |    20210324    |    kiwisolver    |    1.3.1    |    pyqt-impl    |    5.12.3    |
|    absl-py    |    0.13.0    |    koalas    |    1.8.0    |    pyqt5-sip    |    4.19.18    |
|    adal    |    1.2.7    |    krb5    |    1.19.1    |    pyqtchart    |    5.12    |
|    adlfs    |    0.7.7    |    lcms2    |    2.12    |    pyqtwebengine    |    5.12.1    |
|    aiohttp    |    3.7.4.post0    |    ld_impl_linux-64    |    2.36.1    |    pysocks    |    1.7.1    |
|    alsa-lib    |    1.2.3    |    lerc    |    2.2.1    |    python-dateutil    |    2.8.1    |
|    appdirs    |    1.4.4    |    liac-arff    |    2.5.0    |    python-flatbuffers    |    1.12    |
|    arrow-cpp    |    3.0.0    |    libaec    |    1.0.5    |    python_abi    |    3.8    |
|    astor    |    0.8.1    |    libblas    |    3.9.0    |    pytz    |    2021.1    |
|    astunparse    |    1.6.3    |    libbrotlicommon    |    1.0.9    |    pyu2f    |    0.1.5    |
|    async-timeout    |    3.0.1    |    libbrotlidec    |    1.0.9    |    pywavelets    |    1.1.1    |
|    attrs    |    21.2.0    |    libbrotlienc    |    1.0.9    |    pyyaml    |    5.4.1    |
|    aws-c-cal    |    0.5.11    |    libcblas    |    3.9.0    |    pyzmq    |    22.1.0    |
|    aws-c-common    |    0.6.2    |    libclang    |    11.1.0    |    qt    |    5.12.9    |
|    aws-c-event-stream    |    0.2.7    |    libcurl    |    7.77.0    |    re2    |    2021.04.01    |
|    aws-c-io    |    0.10.5    |    libdeflate    |    1.7    |    readline    |    8.1    |
|    aws-checksums    |    0.1.11    |    libedit    |    3.1.20210216    |    regex    |    2021.7.6    |
|    aws-sdk-cpp    |    1.8.186    |    libev    |    4.33    |    requests    |    2.25.1    |
|    azure-datalake-store    |    0.0.51    |    libevent    |    2.1.10    |    requests-oauthlib    |    1.3.0    |
|    azure-identity    |    2021.03.15b1    |    libffi    |    3.3    |    retrying    |    1.3.3    |
|    azure-storage-blob    |    12.8.1    |    libgcc-ng    |    9.3.0    |    rsa    |    4.7.2    |
|    backcall    |    0.2.0    |    libgfortran-ng    |    9.3.0    |    ruamel_yaml    |    0.15.100    |
|    backports    |    1    |    libgfortran5    |    9.3.0    |    s2n    |    1.0.10    |
|    backports.functools_lru_cache    |    1.6.4    |    libglib    |    2.68.3    |    salib    |    1.3.11    |
|    beautifulsoup4    |    4.9.3    |    libiconv    |    1.16    |    scikit-image    |    0.18.1    |
|    blas    |    2.109    |    liblapack    |    3.9.0    |    scikit-learn    |    0.23.2    |
|    blas-devel    |    3.9.0    |    liblapacke    |    3.9.0    |    scipy    |    1.5.3    |
|    blinker    |    1.4    |    libllvm10    |    10.0.1    |    seaborn    |    0.11.1    |
|    blosc    |    1.21.0    |    libllvm11    |    11.1.0    |    seaborn-base    |    0.11.1    |
|    bokeh    |    2.3.2    |    libnghttp2    |    1.43.0    |    setuptools    |    49.6.0    |
|    brotli    |    1.0.9    |    libogg    |    1.3.5    |    shap    |    0.39.0    |
|    brotli-bin    |    1.0.9    |    libopus    |    1.3.1    |    six    |    1.16.0    |
|    brotli-python    |    1.0.9    |    libpng    |    1.6.37    |    skl2onnx    |    1.8.0.1    |
|    brotlipy    |    0.7.0    |    libpq    |    13.3    |    sklearn-pandas    |    2.2.0    |
|    brunsli    |    0.1    |    libprotobuf    |    3.15.8    |    slicer    |    0.0.7    |
|    bzip2    |    1.0.8    |    libsodium    |    1.0.18    |    smart_open    |    5.1.0    |
|    c-ares    |    1.17.1    |    libssh2    |    1.9.0    |    smmap    |    3.0.5    |
|    ca-certificates    |    2021.7.5    |    libstdcxx-ng    |    9.3.0    |    snappy    |    1.1.8    |
|    cachetools    |    4.2.2    |    libthrift    |    0.14.1    |    soupsieve    |    2.2.1    |
|    cairo    |    1.16.0    |    libtiff    |    4.2.0    |    sqlite    |    3.36.0    |
|    certifi    |    2021.5.30    |    libutf8proc    |    2.6.1    |    statsmodels    |    0.12.2    |
|    cffi    |    1.14.5    |    libuuid    |    2.32.1    |    tabulate    |    0.8.9    |
|    chardet    |    4.0.0    |    libuv    |    1.41.1    |    tenacity    |    7.0.0    |
|    charls    |    2.2.0    |    libvorbis    |    1.3.7    |    tensorboard    |    2.4.1    |
|    click    |    8.0.1    |    libwebp-base    |    1.2.0    |    tensorboard-plugin-wit    |    1.8.0    |
|    cloudpickle    |    1.6.0    |    libxcb    |    1.14    |    tensorflow    |    2.6.3    |
|    conda    |    4.9.2    |    libxgboost    |    1.4.0    |    tensorflow-base    |    2.6.3    |
|    conda-package-handling    |    1.7.3    |    libxkbcommon    |    1.0.3    |    tensorflow-estimator    |    2.6.3    |
|    configparser    |    5.0.2    |    libxml2    |    2.9.12    |    termcolor    |    1.1.0    |
|    cryptography    |    3.4.7    |    libzopfli    |    1.0.3    |    textblob    |    0.15.3    |
|    cudatoolkit    |    11.1.1    |    lightgbm    |    3.2.1    |    threadpoolctl    |    2.1.0    |
|    cycler    |    0.10.0    |    lime    |    0.2.0.1    |    tifffile    |    2021.4.8    |
|    cython    |    0.29.23    |    llvm-openmp    |    11.1.0    |    tk    |    8.6.10    |
|    cytoolz    |    0.11.0    |    llvmlite    |    0.36.0    |    toolz    |    0.11.1    |
|    dash    |    1.20.0    |    locket    |    0.2.1    |    tornado    |    6.1    |
|    dash-core-components    |    1.16.0    |    lz4-c    |    1.9.3    |    tqdm    |    4.61.2    |
|    dash-html-components    |    1.1.3    |    markdown    |    3.3.4    |    traitlets    |    5.0.5    |
|    dash-renderer    |    1.9.1    |    markupsafe    |    2.0.1    |    typing-extensions    |    3.10.0.0    |
|    dash-table    |    4.11.3    |    matplotlib    |    3.4.2    |    typing_extensions    |    3.10.0.0    |
|    dash_cytoscape    |    0.2.0    |    matplotlib-base    |    3.4.2    |    unixodbc    |    2.3.9    |
|    dask-core    |    2021.6.2    |    matplotlib-inline    |    0.1.2    |    urllib3    |    1.26.7    |
|    databricks-cli    |    0.12.1    |    mkl    |    2021.2.0    |    wcwidth    |    0.2.5    |
|    dataclasses    |    0.8    |    mkl-devel    |    2021.2.0    |    webencodings    |    0.5.1    |
|    dbus    |    1.13.18    |    mkl-include    |    2021.2.0    |    werkzeug    |    2.0.1    |
|    debugpy    |    1.3.0    |    mleap    |    0.17.0    |    wheel    |    0.36.2    |
|    decorator    |    4.4.2    |    mlflow-skinny    |    1.18.0    |    wrapt    |    1.12.1    |
|    dill    |    0.3.4    |    msal    |    2021.06.08    |    xgboost    |    1.4.0    |
|    entrypoints    |    0.3    |    msal-extensions    |    2021.06.08    |    xorg-kbproto    |    1.0.7    |
|    et_xmlfile    |    1.1.0    |    msrest    |    2021.06.01    |    xorg-libice    |    1.0.10    |
|    expat    |    2.4.1    |    multidict    |    5.1.0    |    xorg-libsm    |    1.2.3    |
|    fire    |    0.4.0    |    mysql-common    |    8.0.25    |    xorg-libx11    |    1.7.2    |
|    flask    |    2.0.1    |    mysql-libs    |    8.0.25    |    xorg-libxext    |    1.3.4    |
|    flask-compress    |    1.10.1    |    ncurses    |    6.2    |    xorg-libxrender    |    0.9.10    |
|    fontconfig    |    2.13.1    |    networkx    |    2.5.1    |    xorg-renderproto    |    0.11.1    |
|    freetype    |    2.10.4    |    ninja    |    1.10.2    |    xorg-xextproto    |    7.3.0    |
|    fsspec    |    2021.6.1    |    nltk    |    3.6.5    |    xorg-xproto    |    7.0.31    |
|    future    |    0.18.2    |    nspr    |    4.3    |    xz    |    5.2.5    |
|    gast    |    0.3.3    |    nss    |    3.67    |    yaml    |    0.2.5    |
|    gensim    |    3.8.3    |    numba    |    0.53.1    |    yarl    |    1.6.3    |
|    geographiclib    |    1.52    |    numpy    |    1.19.4    |    zeromq    |    4.3.4    |
|    geopy    |    2.1.0    |    oauthlib    |    3.1.1    |    zfp    |    0.5.5    |
|    gettext    |    0.21.0    |    olefile    |    0.46    |    zipp    |    3.5.0    |
|    gevent    |    21.1.2    |    onnx    |    1.9.0    |    zlib    |    1.2.11    |
|    gflags    |    2.2.2    |    onnxconverter-common    |    1.7.0    |    zope.event    |    4.5.0    |
|    giflib    |    5.2.1    |    onnxmltools    |    1.7.0    |    zope.interface    |    5.4.0    |
|    gitdb    |    4.0.7    |    onnxruntime    |    1.7.2    |    zstd    |    1.4.9    |
|    gitpython    |    3.1.18    |    openjpeg    |    2.4.0    |    azure-common    |    1.1.27    |
|    glib    |    2.68.3    |    openpyxl    |    3.0.7    |    azure-core    |    1.16.0    |
|    glib-tools    |    2.68.3    |    openssl    |    1.1.1k    |    azure-graphrbac    |    0.61.1    |
|    glog    |    0.5.0    |    opt_einsum    |    3.3.0    |    azure-mgmt-authorization    |    0.61.0    |
|    gobject-introspection    |    1.68.0    |    orc    |    1.6.7    |    azure-mgmt-containerregistry    |    8.0.0    |
|    google-auth    |    1.32.1    |    packaging    |    21    |    azure-mgmt-core    |    1.3.0    |
|    google-auth-oauthlib    |    0.4.1    |    pandas    |    1.2.3    |    azure-mgmt-keyvault    |    2.2.0    |
|    google-pasta    |    0.2.0    |    parquet-cpp    |    1.5.1    |    azure-mgmt-resource    |    13.0.0    |
|    greenlet    |    1.1.0    |    parso    |    0.8.2    |    azure-mgmt-storage    |    11.2.0    |
|    grpc-cpp    |    1.37.1    |    partd    |    1.2.0    |    azureml-core    |    1.29.0.post1    |
|    grpcio    |    1.37.1    |    patsy    |    0.5.1    |    azureml-mlflow    |    1.29.0    |
|    gst-plugins-base    |    1.18.4    |    pcre    |    8.45    |    backports-tempfile    |    1    |
|    gstreamer    |    1.18.4    |    pexpect    |    4.8.0    |    backports-weakref    |    1.0.post1    |
|    h5py    |    2.10.0    |    pickleshare    |    0.7.5    |    contextlib2    |    0.6.0.post1    |
|    hdf5    |    1.10.6    |    pillow    |    8.3.2    |    docker    |    4.4.4    |
|    html5lib    |    1.1    |    pip    |    21.1.1    |    jeepney    |    0.6.0    |
|    hummingbird-ml    |    0.4.0    |    pixman    |    0.40.0    |    jmespath    |    0.10.0    |
|    icu    |    68.1    |    plotly    |    4.14.3    |    jsonpickle    |    2.0.0    |
|    idna    |    2.1    |    pmdarima    |    1.8.2    |    msrestazure    |    0.6.4    |
|    imagecodecs    |    2021.3.31    |    pooch    |    1.4.0    |    mypy    |    0.78    |
|    imageio    |    2.9.0    |    portalocker    |    1.7.1    |    mypy-extensions    |    0.4.3    |
|    importlib-metadata    |    4.6.1    |    prompt-toolkit    |    3.0.19    |    ndg-httpsclient    |    0.5.1    |
|    intel-openmp    |    2021.2.0    |    protobuf    |    3.15.8    |    pandasql    |    0.7.3    |
|    interpret    |    0.2.4    |    psutil    |    5.8.0    |    pathspec    |    0.8.1    |
|    interpret-core    |    0.2.4    |    ptyprocess    |    0.7.0    |    ruamel-yaml    |    0.17.4    |
|    ipykernel    |    6.0.1    |    py-xgboost    |    1.4.0    |    ruamel-yaml-clib    |    0.2.6    |
|    ipython    |    7.23.1    |    py4j    |    0.10.9    |    secretstorage    |    3.3.1    |
|    ipython_genutils    |    0.2.0    |    pyarrow    |    3.0.0    |    sqlalchemy    |    1.4.20    |
|    isodate    |    0.6.0    |    pyasn1    |    0.4.8    |    typed-ast    |    1.4.3    |
|    itsdangerous    |    2.0.1    |    pyasn1-modules    |    0.2.8    |    torchvision    |    0.9.1    |
|    jdcal    |    1.4.1    |    pycairo    |    1.20.1    |    websocket-client    |    1.1.0    |
|    jedi    |    0.18.0    |    pycosat    |    0.6.3    |        |        |
|    jinja2    |    3.0.1    |    pycparser    |    2.2    |        |        |
|    joblib    |    1.0.1    |    pygments    |    2.9.0    |        |        |
|    jpeg    |    9d    |    pygobject    |    3.40.1    |        |        |
|    jupyter_client    |    6.1.12    |    pyjwt    |    2.1.0    |        |        |
|    jupyter_core    |    4.7.1    |    pyodbc    |    4.0.30    |        |        |
|    jxrlib    |    1.1    |    pyopenssl    |    20.0.1    |        |        |
|    pyspark    |    3.1.2    |    pyparsing    |    2.4.7    |        |        |


## Runtime for Apache Spark release 2022.1 (BDC.3.2022.1) - Installed R libraries

_Packages from [CRAN Snapshot 2021-09-21](https://cran.microsoft.com/snapshot/2021-09-21)_

The following table lists packages available in this release.

|package|package|package|package|package|package|
|--|--|--|--|--|--|
sparklyr | earth | lifecycle | bit | markdown | reshape2 |
devtools | forecast | ellipsis | bit64 | parallelly | rmarkdown |
tidyverse | futile.logger | pillar | Matrix | pROC | RSQLite |
igraph | hash | cli | survival | ps | rvest |
plotrix | ids | tibble | mgcv | readr | sessioninfo |
mnormt | jsonlite | glue | lattice | rematch | sourcetools |
hts | kernlab | magrittr | emmeans | roxygen2 | stringi |
Rlabkey | lubridate | Rcpp | cluster | Rserve | sys |
gtools | mgcv | cpp11 | nnet | sqldf | xml2 |
svMisc | minpack.lm | crayon | boot | timeDate | yaml |
uuid | nlme | tidyr | estimability | waldo | backports |
plotmo | pls | tidyselect | class | brew | data.table |
mgsub | prophet | fansi | KernSmooth | curl | dbplyr |
Hmisc | purrr | utf8 | spatial | DBI | forcats |
MASS | reticulate | R6 | rpart | ggplot2 | globals |
MSwM | rstan | pkgconfig | arrow | glmnet | httr |
Rmpfr | rstatix | generics | codetools | gower | iterators |
anomalize | smooth | lme4 | foreign | gtable | modelr |
arm | sn | RcppEigen | broom | hms | pkgbuild |
bsts | stringr | plyr | cellranger | ModelMetrics | prodlim |
caret | strucchange | nloptr | foreach | pkgload | recipes |
changepoint | vars | minqa | fs | processx | remotes |
dlm | xts | mvtnorm | gsubfn | randomForest | rex |
dplyr | zoo | assertthat | hwriter | RColorBrewer | RODBC |
dtwclust | rlang | numDeriv | ipred | readxl | scales |
e1071 | vctrs | xtable | labeling | rematch2 | shape |


## SQL Server Machine Learning Services - Installed Python libraries

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

## SQL Server Machine Learning Services - Installed R libraries

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

For more information about [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md)
