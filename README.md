# denon-control

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/prayzzz/denon-control/CI)
[![Docker Image Version (latest semver)](https://img.shields.io/docker/v/prayzzz/denon-control?sort=semver)](https://hub.docker.com/r/prayzzz/denon-control)

Web UI to control your Denon AVR.

## Technologies

* .NET Core (https://github.com/dotnet/core)
* ASP.NET Core & Blazor (https://github.com/dotnet/aspnetcore)

## Screenshot

![](.github/web-interface.png)

## Run it

### docker-compose
```yaml
version: '3'

services:
  denon-control:
    image: prayzzz/denon-control:latest
    container_name: denon-control
    port: 8080:80
#    volumes:
#     - ./appsettings.json:/app/appsettings.Production.json
```

## Docs
* Denon Control Protocol (rev. 2020-07-29)
  * http://assets.denon.com/_layouts/15/xlviewer.aspx?id=/DocumentMaster/us/FY21AVR_DENON_PROTOCOL_02092020.xlsx
