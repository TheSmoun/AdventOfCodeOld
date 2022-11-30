# 2022 Advent of Code

This repository contains my private solutions for [Advent of Code](https://adventofcode.com) 2022.

## Setup

To setup the DB, first you need to create a .env file within the infrastructure folder. This file needs to contain the following properties.
```
DOCKER_REGISTRY: The URL of the Docker registry to provide the Docker image for the Oracle database to use.
SYSTEM_PWD: The password of the SYSTEM user within your database.
```

After that you can simply run the containers with `docker-compose up -d`.
