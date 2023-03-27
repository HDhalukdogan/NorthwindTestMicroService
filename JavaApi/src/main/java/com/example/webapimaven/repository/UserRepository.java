package com.example.webapimaven.repository;

import com.example.webapimaven.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<User, Long> {
}

