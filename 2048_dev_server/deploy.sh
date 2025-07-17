#!/bin/bash

dotnet publish -c Release -r linux-x64 --self-contained false -o publish

# 1. 빌드 결과물을 서버로 복사
scp -i whawoo-2048-server-key.pem -r ./publish/* myserver:/home/ubuntu/myserver/

# 2. 서버에서 서비스 재시작
ssh -i whawoo-2048-server-key.pem myserver 'sudo systemctl restart my-server-service'
