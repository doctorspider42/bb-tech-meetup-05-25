import React, { createContext, useContext, useState, useCallback } from 'react';
import axios from 'axios';

// Create Axios instance with proper configuration
const apiClient = axios.create({
  baseURL: '/',
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json',
  }
});

// Create context for email analysis
const EmailContext = createContext();

export const EmailProvider = ({ children }) => {
  const [emailContent, setEmailContent] = useState('');
  const [response, setResponse] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [showConfirmation, setShowConfirmation] = useState(false);
  // Process email through API
  const processEmail = useCallback(async () => {
    if (!emailContent.trim()) return;
    
    setLoading(true);
    setError('');
    setResponse(null);
    setShowConfirmation(false);
    
    try {
      const result = await apiClient.post('/ai/mail', { emailContent });
      // Make sure we're properly capturing the response data
      console.log('API Response:', result); // Debug log to check the response structure
      
      // Check if response exists and set it properly
      if (result.data) {
        setResponse(result.data);
      } else {
        throw new Error('No response data received from the server');
      }
    } catch (err) {
      console.error('API Error:', err); // Debug log for errors
      setError(err.response?.data?.title || err.message || 'An error occurred while processing your request.');
    } finally {
      setLoading(false);
    }
  }, [emailContent]);

  // Clear the form and reset state
  const resetForm = useCallback(() => {
    setEmailContent('');
    setResponse(null);
    setError('');
  }, []);

  const value = {
    emailContent,
    setEmailContent,
    response,
    loading,
    error,
    showConfirmation,
    setShowConfirmation,
    processEmail,
    resetForm
  };

  return <EmailContext.Provider value={value}>{children}</EmailContext.Provider>;
};

// Custom hook to use the email context
export const useEmail = () => {
  const context = useContext(EmailContext);
  if (context === undefined) {
    throw new Error('useEmail must be used within an EmailProvider');
  }
  return context;
};

export default EmailContext;
