pipeline {
    agent any
    triggers {
        pollSCM("*/5 * * * *")
    }
    environment {
        COMMITMSG = sh(returnStdout: true, script: "git log -1 --oneline")
    }
    stages {
        stage("Start up"){
            when {
                anyOf {
                    changeset "Api/**"
                    changeset "Domain.Test/**"
                    changeset "Domain/**"
                    changeset "DataAccess/**"
                    changeset "Core.Test/**"
                    changeset "Core/**"
                }
            }
            steps {
                withCredentials([string(credentialsId: 'DiscordWebHook', variable: 'WEBHOOK_URL')]) {
                    discordSend description: "Jenkins Pipeline Start", footer: env.COMMITMSG , link: env.Build_URL, webhookURL: "${WEBHOOK_URL}"
                    buildDescription env.COMMITMSG
                }
            }
        }
        stage("Build Api"){
            when {
                anyOf {
                    changeset "Api/**"
                    changeset "Domain.Test/**"
                    changeset "Domain/**"
                    changeset "DataAccess/**"
                    changeset "Core.Test/**"
                    changeset "Core/**"
                }
            }
            steps {
                echo "We are building"
                dir("Api"){
                    sh "dotnet build --configuration Release"
                }
            }
        }
        stage("Unit Test"){
            steps {
                    dir("Core.Test") {
                        sh "dotnet add package coverlet.collector"
                        sh "dotnet test --collect:'XPlat Code Coverage'"
                    }
                }
                post {
                    success {
                        publishCoverage adapters: [coberturaAdapter('Core.Test/TestResults/*/coverage.cobertura.xml')], sourceFileResolver: sourceFiles('NEVER_STORE')
                    }
            }
        }
        post {
        always {
            withCredentials([string(credentialsId: 'DiscordWebHook', variable: 'WEBHOOK_URL')]) {
                sh "echo 'Pipeline finished!'"
                discordSend description: "Jenkins Pipeline Finished", footer: "The pipeline has finished!", link: env.Build_URL, result: currentBuild.currentResult, title: JOB_NAME, webhookURL: "${WEBHOOK_URL}"
            }
        }
        success {
            withCredentials([string(credentialsId: 'DiscordWebHook', variable: 'WEBHOOK_URL')]) {
                sh "echo 'Pipeline finished!'"
                discordSend description: "Jenkins Pipeline Success", footer: "Pipeline finished successfully", link: env.Build_URL, result: currentBuild.currentResult, title: JOB_NAME, webhookURL: "${WEBHOOK_URL}"
            }
        }
        failure {
            withCredentials([string(credentialsId: 'DiscordWebHook', variable: 'WEBHOOK_URL')]) {
                sh "echo 'Pipeline finished!'"
                discordSend description: "Jenkins Pipeline Failed", footer: "Pipeline failed", link: env.Build_URL, result: currentBuild.currentResult, title: JOB_NAME, webhookURL: "${WEBHOOK_URL}"
            }
        }
        unstable {
            withCredentials([string(credentialsId: 'DiscordWebHook', variable: 'WEBHOOK_URL')]) {
                sh "echo 'Pipeline finished!'"
                discordSend description: "Jenkins Pipeline Unstable", footer: "Pipeline marked unstable", link: env.Build_URL, result: currentBuild.currentResult, title: JOB_NAME, webhookURL: "${WEBHOOK_URL}"
            }
        }
        changed {
            withCredentials([string(credentialsId: 'DiscordWebHook', variable: 'WEBHOOK_URL')]) {
                sh "echo 'Pipeline finished!'"
                discordSend description: "Jenkins Pipeline Changed", footer: "Pipeline's state changed", link: env.Build_URL, result: currentBuild.currentResult, title: JOB_NAME, webhookURL: "${WEBHOOK_URL}"
            }
        }
    }
    }
}