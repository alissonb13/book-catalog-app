# Stage 1: Build da aplicação
FROM node:20-alpine AS build

# Diretório de trabalho dentro do container
WORKDIR /app

# Copia package.json e package-lock.json / yarn.lock
COPY package*.json ./

# Instala dependências
RUN npm install

# Copia o restante do código
COPY . .

# Build otimizado da aplicação
RUN npm run build

# Stage 2: Servir a aplicação com Nginx
FROM nginx:alpine

# Remove configuração default do Nginx
RUN rm -rf /usr/share/nginx/html/*

# Copia build do stage anterior para diretório do Nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Copia configuração customizada do Nginx (opcional)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expõe a porta padrão do Nginx
EXPOSE 80

# Comando para rodar o Nginx em foreground
CMD ["nginx", "-g", "daemon off;"]
