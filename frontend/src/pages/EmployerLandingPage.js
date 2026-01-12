import { useNavigate } from 'react-router-dom';
import styles from './EmployerLandingPage.module.css';

const EmployerLandingPage = () => {
  const navigate = useNavigate();

  const handlePostInternship = () => {
    navigate('/auth/registration'); // Або '/register?role=employer'
  };

  const features = [
    {
      icon: 'bi-people-fill',
      title: 'Access Thousands of Candidates',
      description: 'Connect with motivated students actively seeking internship opportunities'
    },
    {
      icon: 'bi-cash-coin',
      title: 'Post for Free',
      description: 'No hidden fees. Publish unlimited internship positions at zero cost'
    },
    {
      icon: 'bi-clock-history',
      title: 'Save Time',
      description: 'Streamlined application process helps you find the right talent faster'
    },
    {
      icon: 'bi-star-fill',
      title: 'Quality Candidates',
      description: 'Access verified student profiles with complete resumes and portfolios'
    },
    {
      icon: 'bi-graph-up-arrow',
      title: 'Build Your Team',
      description: 'Discover fresh talent and future full-time employees'
    },
    {
      icon: 'bi-shield-check',
      title: 'Trusted Platform',
      description: 'Join 35,000+ companies already hiring through Internships.com'
    }
  ];

  const stats = [
    { number: '108,000+', label: 'Active Students' },
    { number: '35,000+', label: 'Companies' },
    { number: '43,000+', label: 'Success Stories' },
    { number: '100%', label: 'Free to Post' }
  ];

  const steps = [
    {
      number: '1',
      title: 'Create Your Account',
      description: 'Sign up in less than 2 minutes with your company email'
    },
    {
      number: '2',
      title: 'Post Your Internship',
      description: 'Fill in the details about your internship opportunity'
    },
    {
      number: '3',
      title: 'Review Applications',
      description: 'Browse qualified candidates and connect with top talent'
    },
    {
      number: '4',
      title: 'Hire Great Interns',
      description: 'Find the perfect match for your team and start onboarding'
    }
  ];

  return (
    <div className={styles.pageWrapper}>
      {/* Hero Section */}
      <section className={styles.heroSection}>
        <div className={styles.container}>
          <div className={styles.heroContent}>
            <h1 className={styles.heroTitle}>
              Find Your Next <span className={styles.highlight}>Top Intern</span>
            </h1>
            <p className={styles.heroSubtitle}>
              Thousands of talented students are actively searching for internship opportunities. 
              Connect with them today — completely free.
            </p>
            <button className={styles.ctaButton} onClick={handlePostInternship}>
              <i className="bi bi-plus-circle-fill"></i>
              Post Your Internship Free Now
            </button>
            <p className={styles.ctaNote}>
              <i className="bi bi-check-circle-fill"></i>
              No credit card required • Takes less than 5 minutes
            </p>
          </div>
        </div>
      </section>

      {/* Stats Section */}
      <section className={styles.statsSection}>
        <div className={styles.container}>
          <div className={styles.statsGrid}>
            {stats.map((stat, index) => (
              <div key={index} className={styles.statCard}>
                <div className={styles.statNumber}>{stat.number}</div>
                <div className={styles.statLabel}>{stat.label}</div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className={styles.featuresSection}>
        <div className={styles.container}>
          <div className={styles.sectionHeader}>
            <h2 className={styles.sectionTitle}>Why Choose Internships.com?</h2>
            <p className={styles.sectionSubtitle}>
              Everything you need to find and hire exceptional interns
            </p>
          </div>
          <div className={styles.featuresGrid}>
            {features.map((feature, index) => (
              <div key={index} className={styles.featureCard}>
                <div className={styles.featureIcon}>
                  <i className={`bi ${feature.icon}`}></i>
                </div>
                <h3 className={styles.featureTitle}>{feature.title}</h3>
                <p className={styles.featureDescription}>{feature.description}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* How It Works Section */}
      <section className={styles.stepsSection}>
        <div className={styles.container}>
          <div className={styles.sectionHeader}>
            <h2 className={styles.sectionTitle}>How It Works</h2>
            <p className={styles.sectionSubtitle}>
              Four simple steps to finding your ideal intern
            </p>
          </div>
          <div className={styles.stepsGrid}>
            {steps.map((step, index) => (
              <div key={index} className={styles.stepCard}>
                <div className={styles.stepNumber}>{step.number}</div>
                <h3 className={styles.stepTitle}>{step.title}</h3>
                <p className={styles.stepDescription}>{step.description}</p>
                {index < steps.length - 1 && (
                  <div className={styles.stepArrow}>
                    <i className="bi bi-arrow-right"></i>
                  </div>
                )}
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* Testimonial Section */}
      <section className={styles.testimonialSection}>
        <div className={styles.container}>
          <div className={styles.testimonialCard}>
            <div className={styles.quoteIcon}>
              <i className="bi bi-quote"></i>
            </div>
            <p className={styles.testimonialText}>
              "We've hired 15 interns through Internships.com in the past year. 
              The quality of candidates is exceptional, and the platform is incredibly easy to use. 
              Three of our interns are now full-time employees!"
            </p>
            <div className={styles.testimonialAuthor}>
              <div className={styles.authorAvatar}>
                <i className="bi bi-person-circle"></i>
              </div>
              <div>
                <div className={styles.authorName}>Sarah Johnson</div>
                <div className={styles.authorTitle}>HR Manager, Tech Solutions Inc.</div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Final CTA Section */}
      <section className={styles.finalCtaSection}>
        <div className={styles.container}>
          <div className={styles.finalCtaCard}>
            <h2 className={styles.finalCtaTitle}>
              Ready to Find Your Next Great Intern?
            </h2>
            <p className={styles.finalCtaText}>
              Join thousands of companies already hiring on Internships.com
            </p>
            <button className={styles.ctaButtonLarge} onClick={handlePostInternship}>
              <i className="bi bi-rocket-takeoff-fill"></i>
              Get Started — It's Free!
            </button>
            <div className={styles.trustBadges}>
              <div className={styles.trustBadge}>
                <i className="bi bi-shield-check"></i>
                <span>Secure Platform</span>
              </div>
              <div className={styles.trustBadge}>
                <i className="bi bi-currency-dollar"></i>
                <span>100% Free</span>
              </div>
              <div className={styles.trustBadge}>
                <i className="bi bi-clock"></i>
                <span>24/7 Support</span>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default EmployerLandingPage;