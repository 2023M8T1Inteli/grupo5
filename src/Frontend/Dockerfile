# Use a imagem do Node.js para construir o projeto
FROM node:alpine AS builder
WORKDIR /src

# Copie os arquivos do projeto
# USER 0
# RUN mkdir -p /src/node_modules
COPY src/Frontend/package.json src/Frontend/package-lock.json ./
RUN npm install

# Construa o projeto
COPY src/Frontend/ ./
RUN npm run build

# Use a imagem do servidor do Node.js para rodar o projeto
FROM node:alpine
WORKDIR /src
COPY --from=builder /src/next.config.js ./
COPY --from=builder /src/.next ./.next
COPY --from=builder /src/public ./public
COPY --from=builder /src/package.json ./package.json

RUN npm install next

# Exponha a porta que o Next.js usa
EXPOSE 3000

CMD ["npm", "start"]
