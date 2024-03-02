// services/api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7286/api/Rooms', // Thay đổi thành địa chỉ API thực tế của bạn
});

export default api;