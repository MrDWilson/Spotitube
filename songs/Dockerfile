FROM node

WORKDIR /usr/src/app
COPY package*.json ./
RUN mkdir patches
COPY patches/* patches/
RUN npm install
COPY . .
CMD [ "node", "server.js" ]
