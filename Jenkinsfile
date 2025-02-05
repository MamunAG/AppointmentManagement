pipeline {
    agent any
 
    environment {
        registryCredentialUp = 'docker_up'
        registry_appointmentManagement = 'repoNameAppointmentManagement'
        dockerImage_appointmentManagement = ''
    }

    stages {
        stage('Start') {
            steps {
                echo 'Pipeline start.'
            }
        }
    
        stage('Building image: appointmentManagement') {
            steps{
                script {
                    sh "ls"
                     dockerImage_appointmentManagement = docker.build(registry_appointmentManagement + ":$BUILD_NUMBER", "-f ${dockerfile_appointmentManagement} .")
                }
            }
        }

        stage('Push to dockerhub- appointmentManagement') {
            steps{
                script {
                    docker.withRegistry( '', registryCredentialUp ) {
                        dockerImage_appointmentManagement.push()
                    }   
                }
            }
        }

        stage('Cleaning up') {
            steps{
                sh "docker rmi $registry_appointmentManagement:$BUILD_NUMBER"
            }
        }

        stage('Tigger CD Pipeline new') {
            steps{
                script {
                    def imageTag = env.BUILD_NUMBER
                    sh """
                            curl -X POST "http://localhost:8080/job/appointment-management-cd-pipeline/buildWithParameters?token=jenkins" \
                                 --data-urlencode "IMAGE_TAG=${imageTag}" \
                                 --user jenkins:115f5bd75ec12670df2ae2068c8f3db59f
                        """
                }
            }
        }
    }
 
    post {
        success {
            echo 'Docker image build and push successful!'
        }
        failure {
            error 'Docker image build or push failed!'
        }
    }
}