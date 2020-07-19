# Running the docker

5656 - Binary protocol

```
# Mounting the volume
-v <datadir>:/var/lib/edgedb/data \

docker run -it --rm --name=edgedb-server \
    -p 5656:5656 -p 8888:8888 -p 8889:8889 \
    edgedb/edgedb
```


### Downloading the CLI

```
curl https://packages.edgedb.com/dist/linux-x86_64/edgedb-cli_latest > ./edgedb
chmod +x ./edgedb
```


### First Login

```
edgedb -H localhost -u edgedb


CONFIGURE SYSTEM INSERT Port {
    protocol := "graphql+http",
    database := "sulphur",
    address := "0.0.0.0",
    port := 8888,
    user := "http",
    concurrency := 4,
};

```
