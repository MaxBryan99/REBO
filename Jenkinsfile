pipeline {
    agent any
    tools {
        maven "Maven"
    }

    stages {
        stage('Build Artifact') {
            steps {
                bat "mvn clean package -DskipTests=true"
                archiveArtifacts artifacts: 'target/*.jar', fingerprint: true
            }
        }
        stage('Test Maven - JUnit') {
            steps {
                bat "mvn test"
            }
            post {
                always {
                    junit 'target/surefire-reports/*.xml'
                }
            }
        }
        stage('Sonarqube Analysis - SAST') {
            steps {
                withSonarQubeEnv('SonarQube') {
                    bat """mvn clean verify sonar:sonar \
                        -Dsonar.projectKey=maven-jenkins-pipeline-Rebo \
                        -Dsonar.host.url=http://localhost:9000/"""
                }
            }
        }
    }
}