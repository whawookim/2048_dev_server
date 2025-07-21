#!/bin/bash

ssh -i whawoo-2048-server-key.pem myserver 'rm -rf ~/2048_dev_server'

# 1. 전체 프로젝트 동기화
scp -i whawoo-2048-server-key.pem -r ../2048_dev_server myserver:/home/ubuntu/

# 2. 서버에서 서비스 재시작
ssh -i whawoo-2048-server-key.pem myserver 'cd /home/ubuntu/2048_dev_server && dotnet publish -c Release -r linux-x64 --self-contained false -o ../myserver && sudo systemctl restart my-server-service'

ssh -i whawoo-2048-server-key.pem myserver 'cd /home/ubuntu/2048_dev_server && dotnet ef database update'