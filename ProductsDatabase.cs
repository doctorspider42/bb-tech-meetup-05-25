using System.Collections.Generic;

namespace ProjectOllama
{
    /// <summary>
    /// Database of fictional product information for the ProjectOllama application
    /// </summary>
    public class ProductsDatabase
    {
        /// <summary>
        /// Dictionary containing product information with product name as key
        /// </summary>
        public static readonly Dictionary<string, string> Products = new Dictionary<string, string>
        {
            {
                "SUPER2000",
                "SUPER2000 is an enterprise-grade project management solution designed for medium to large businesses. " +
                "PRICING: Base price $499.99, with $29.99 per user per month. Discounts available: 15% off for 10+ users, " +
                "25% off for 50+ users, and 35% off for 100+ users. " +
                "INTEGRATIONS: Seamlessly connects with Slack, Microsoft Teams, GitHub, GitLab, Jira, Salesforce, HubSpot, " +
                "QuickBooks, Xero, and Zapier, allowing for flexible workflows across your entire tech stack. " +
                "FEATURES: Offers Gantt Charts, Kanban Boards, Time Tracking, Resource Management, Budget Tracking, " +
                "Risk Assessment, Custom Dashboards, API Access, and Mobile Apps, providing comprehensive project oversight. " +
                "TRIAL VERSION: 30-day free trial available with no credit card required. " +
                "SYSTEM REQUIREMENTS: Runs on Windows 10/11, macOS 12+, or Linux with 8GB RAM and 2GHz quad-core processor minimum. " +
                "SUPPORT OPTIONS: Includes 24/7 Email Support, Business Hours Phone Support, extensive Knowledge Base, " +
                "Video Tutorials, and access to the Community Forum."
            },
            {
                "DATAMAX PRO",
                "DATAMAX PRO is an advanced data analytics and visualization platform with AI-powered insights and predictive modeling capabilities. " +
                "PRICING: Base license at $799.99 with $49.99 per user monthly. Volume discounts: 10% for 10+ users, 20% for 25+ users, " +
                "and 30% for 50+ users. " +
                "INTEGRATIONS: Compatible with SQL Server, PostgreSQL, MongoDB, Oracle, AWS S3, Google BigQuery, Tableau, " +
                "Power BI, Snowflake, and Hadoop ecosystems. " +
                "FEATURES: Delivers Interactive Dashboards, Real-time Analytics, Predictive Modeling, Machine Learning Integration, " +
                "Natural Language Queries, Automated Reporting, Custom Visualizations, and robust Data Pipelines. " +
                "TRIAL VERSION: 14-day trial available requiring credit card information for registration. " +
                "SYSTEM REQUIREMENTS: Requires Windows 10/11 or macOS 12+ with minimum 16GB RAM, 2.5GHz octa-core processor, " +
                "and 5GB free disk space. " +
                "SUPPORT OPTIONS: Offers Priority Email Support, Dedicated Account Manager service, Implementation Consulting, " +
                "Training Sessions, and comprehensive API Documentation."
            },
            {
                "SECUREDEFEND 360",
                "SECUREDEFEND 360 is a comprehensive cybersecurity solution offering real-time threat detection, vulnerability assessment, " +
                "and network monitoring for enterprise environments. " +
                "PRICING: Starting at $1,299.99 base license plus $12.99 per endpoint monthly. Bulk discounts: " +
                "10% for 50+ endpoints, 15% for 100+ endpoints, and 25% for 500+ endpoints. " +
                "INTEGRATIONS: Works with Active Directory, SIEM Systems, Azure Sentinel, Splunk, PagerDuty, " +
                "ServiceNow, Okta, AWS Security Hub, and Microsoft Defender. " +
                "FEATURES: Includes Real-time Threat Detection, Vulnerability Assessment, Network Monitoring, Endpoint Protection, " +
                "Data Loss Prevention, Access Control, Incident Response, Compliance Reporting, and Threat Intelligence. " +
                "TRIAL VERSION: 21-day trial available without credit card, includes complimentary security assessment. " +
                "SYSTEM REQUIREMENTS: Compatible with Windows Server 2016+, Linux distributions; requires 16GB RAM, " +
                "4-core processor, and 50GB free disk space. " +
                "SUPPORT OPTIONS: Provides 24/7 Live Support, Emergency Response Team access, Security Advisories, " +
                "Quarterly Security Reviews, and Compliance Assistance."
            },
            {
                "CLOUDSCAPE ENTERPRISE",
                "CLOUDSCAPE ENTERPRISE is a cloud infrastructure management platform offering automated provisioning, cost optimization, " +
                "and multi-cloud governance capabilities. " +
                "PRICING: $999.99 base platform fee plus $0.05 per managed cloud resource monthly. Tiered discounts: " +
                "15% for 1,000+ resources, 25% for 5,000+ resources, and 35% for 10,000+ resources. " +
                "INTEGRATIONS: Supports AWS, Microsoft Azure, Google Cloud Platform, Oracle Cloud, Kubernetes, Docker, " +
                "Terraform, Ansible, Chef, and Puppet. " +
                "FEATURES: Provides Multi-cloud Management, Cost Optimization, Automated Provisioning, Compliance Guardrails, " +
                "Resource Tagging, Performance Monitoring, Security Posture Management, Budget Alerts, and Reserved Instance Management. " +
                "TRIAL VERSION: 30-day trial with credit card registration required, limited to managing 100 cloud resources. " +
                "SYSTEM REQUIREMENTS: Delivered as SaaS-based solution with web browser access; compatible with Chrome, Firefox, " +
                "Safari, and Edge browsers. " +
                "SUPPORT OPTIONS: Includes Technical Support, Cloud Architects on Demand, Migration Assistance, " +
                "Best Practices Consulting, and Architecture Design Reviews."
            },
            {
                "DEVFLOW CI/CD",
                "DEVFLOW CI/CD is an end-to-end DevOps automation platform for continuous integration, deployment, and testing " +
                "with built-in security scanning functionality. " +
                "PRICING: $599.99 platform license plus $39.99 per developer monthly. Discount structure: " +
                "10% for 10+ developers, 20% for 25+ developers, and 30% for 50+ developers. " +
                "INTEGRATIONS: Compatible with GitHub, GitLab, Bitbucket, Jenkins, Docker, Kubernetes, " +
                "JFrog Artifactory, SonarQube, Selenium, JUnit, NUnit, and Jest. " +
                "FEATURES: Provides Automated Build Pipelines, Container Management, Infrastructure as Code, Automated Testing, " +
                "Code Quality Analysis, Release Management, Dependency Scanning, Vulnerability Detection, and Deployment Rollbacks. " +
                "TRIAL VERSION: 14-day free trial without credit card, limited to 1,000 build minutes. " +
                "SYSTEM REQUIREMENTS: Self-hosted option requires Windows Server 2019+ or Linux with 8GB RAM and 4-core processor; " +
                "cloud-hosted option also available. " +
                "SUPPORT OPTIONS: Offers access to Developer Community, Technical Documentation, Implementation Support, " +
                "CI/CD Best Practices guidance, and Plugin Development Support."
            }
        };        /// <summary>
        /// Searches for a product by name, handling case-insensitivity and whitespace trimming/removal
        /// </summary>
        /// <param name="productName">The name of the product to find</param>
        /// <returns>The product description if found, null otherwise</returns>
        public static string? Find(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return null;
            }

            // Normalize the search term by trimming whitespace and converting to uppercase
            string normalizedSearchTerm = productName.Trim().ToUpperInvariant();
            // Also create a version with all spaces removed
            string noSpacesSearchTerm = normalizedSearchTerm.Replace(" ", "");

            // Search for exact match first
            foreach (var kvp in Products)
            {
                string keyUpperCase = kvp.Key.ToUpperInvariant();
                string keyNoSpaces = keyUpperCase.Replace(" ", "");
                
                if (keyUpperCase == normalizedSearchTerm || keyNoSpaces == noSpacesSearchTerm)
                {
                    return kvp.Value;
                }
            }

            // If no exact match, try contains match
            foreach (var kvp in Products)
            {
                string keyUpperCase = kvp.Key.ToUpperInvariant();
                string keyNoSpaces = keyUpperCase.Replace(" ", "");
                
                if (keyUpperCase.Contains(normalizedSearchTerm) || 
                    normalizedSearchTerm.Contains(keyUpperCase) ||
                    keyNoSpaces.Contains(noSpacesSearchTerm) ||
                    noSpacesSearchTerm.Contains(keyNoSpaces))
                {
                    return kvp.Value;
                }
            }

            return null;
        }
    }
}
