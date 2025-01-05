pipeline {
    agent any

    environment {
        PATH = "/usr/bin/dotnet:$PATH"  // Ajouter dotnet à votre PATH si ce n'est pas déjà fait
        DOCKER_IMAGE_NAME = "gestion-bib" // Nom de l'image Docker à utiliser
        DOCKER_TAG = "latest" // Tag de l'image Docker
        REGISTRY = "docker.io" // Docker registry, changez selon votre besoin
        DOCKER_CREDENTIALS_ID = "dockerhub-credentials" // ID des credentials pour Docker Hub
    }

    stages {
        stage('Checkout') {
            steps {
                // Cloner votre dépôt Git
                git credentialsId: 'jenkinsgit', branch: 'main', url: 'https://github.com/MoatazBen/Bibliojenkins.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                // Restaurer les dépendances .NET
                script {
                    sh 'dotnet restore biblio/biblio.sln'
                }
            }
        }

        stage('Build') {
            steps {
                // Construire le projet .NET
                script {
                    sh 'dotnet build biblio/biblio.sln --configuration Release'
                }
            }
        }

        stage('Test') {
            steps {
                // Exécuter les tests avec dotnet
                script {
                    sh 'dotnet test biblio/biblio.sln --configuration Release'
                }
            }
        }

        stage('Publish') {
            steps {
                // Publier le projet .NET
                script {
                    sh 'dotnet publish biblio/biblio.sln -c Release -o out'
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                // Construire l'image Docker
                script {
                    sh '''
                    docker build -t ${DOCKER_IMAGE_NAME}:${DOCKER_TAG} -f biblio/biblio/Dockerfile .
                    '''
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                // Push l'image Docker vers Docker Hub
                script {
                    withCredentials([usernamePassword(credentialsId: "dockerid", usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                        sh '''
                            echo $DOCKER_PASSWORD | docker login -u $DOCKER_USERNAME --password-stdin
                            docker tag ${DOCKER_IMAGE_NAME}:latest "$DOCKER_USERNAME"/${DOCKER_IMAGE_NAME}:latest
                            docker push "$DOCKER_USERNAME"/${DOCKER_IMAGE_NAME}:${DOCKER_TAG}
                        '''
                    }
                }
            }
        }
    }
}
